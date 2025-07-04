using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleBookStore.Data;
using SimpleBookStore.Models;
using SimpleBookStore.Models.ViewModels;
using SimpleBookStore.Service.IService;
using SimpleBookStore.Utility;
using Stripe;
using Stripe.Checkout;

namespace SimpleBookStore.Service
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OrderService(AppDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<OrderHeader> GetOrderAsync(int orderId)
        {
            return await _db.OrderHeaders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.OrderHeaderId == orderId);
        }

        public async Task<OrderHeader> GetOrderHeaderAsync(int orderId)
        {
            return await _db.OrderHeaders
                .FirstOrDefaultAsync(o => o.OrderHeaderId == orderId);
        }

        public async Task<IEnumerable<OrderHeader>> GetOrdersHeaderAsync(string userId)
        {
            return await _db.OrderHeaders
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        public async Task<int> CreateOrderAsync(string userId, RecipientInfo recipient, string? couponCode = null)
        {
            await using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var cartItems = await _db.ShoppingCarts
                    .Where(c => c.UserId == userId)
                    .Include(c => c.Product)
                    .ToListAsync();

                if (!cartItems.Any())
                    throw new InvalidOperationException("購物車為空");

                int orderTotal = cartItems.Sum(item => item.Quantity * item.UnitPrice);
                int discount = 0;
                if (!string.IsNullOrEmpty(couponCode))
                {
                    var coupon = await _db.Coupons.FirstOrDefaultAsync(c => c.Code == couponCode && c.IsActive);
                    if (coupon != null)
                    {
                        discount = coupon.DiscountAmount;
                        orderTotal -= discount;
                    }
                }

                var orderHeader = new OrderHeader
                {
                    UserId = userId,
                    CouponCode = couponCode,
                    Discount = discount,
                    OrderTotal = orderTotal,
                    Name = recipient.Name,
                    Phone = recipient.PhoneNumber,
                    Address = recipient.Address,
                    OrderTime = DateTime.UtcNow.AddHours(8),
                    Status = SD.Status_Pending
                };
                _db.OrderHeaders.Add(orderHeader);

                foreach (var item in cartItems)
                {
                    var orderDetail = new OrderDetails
                    {
                        OrderHeader = orderHeader,
                        ProductId = item.ProductId,
                        Count = item.Quantity,
                        ProductName = item.Product.Title,
                        Price = (double)item.UnitPrice
                    };
                    _db.OrderDetails.Add(orderDetail);
                }

                _db.ShoppingCarts.RemoveRange(cartItems);
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                return orderHeader.OrderHeaderId;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<string> CreateStripeSession(int orderId, string? couponCode)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var domain = $"{request.Scheme}://{request.Host}";
            var successUrl = $"{domain}/Order/Confirmation?orderId=" + orderId;
            var cancelUrl = $"{domain}/Cart/Index";

            var options = new SessionCreateOptions
            {
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl,
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                PaymentMethodTypes = new List<string> { "card" }
            };

            var DiscountsObj = new List<SessionDiscountOptions>()
            {
                new SessionDiscountOptions
                {
                    Coupon=couponCode
                }
            };
            var orderHeader = await _db.OrderHeaders
                .FirstOrDefaultAsync(o => o.OrderHeaderId == orderId);

            var orderDetails = await _db.OrderDetails
                .Where(od => od.OrderHeaderId == orderId)
                .ToListAsync();

            foreach (var item in orderDetails)
            {
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * 100),
                        Currency = "twd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.ProductName
                        }
                    },
                    Quantity = item.Count
                };

                options.LineItems.Add(sessionLineItem);
            }

            if (orderHeader.Discount > 0)
            {
                options.Discounts = DiscountsObj;
            }
            var service = new SessionService();
            Session session = service.Create(options);
            orderHeader.StripeSessionId = session.Id;
            _db.SaveChanges();

            return session.Url;
        }

        public async Task<bool> ValidateStripeSession(int orderId)
        {
            var orderHeader = await _db.OrderHeaders
                .FirstOrDefaultAsync(o => o.OrderHeaderId == orderId);
            var service = new SessionService();
            Session session = service.Get(orderHeader.StripeSessionId);

            var paymentIntentService = new PaymentIntentService();
            PaymentIntent paymentIntent = paymentIntentService.Get(session.PaymentIntentId);

            if (paymentIntent.Status == "succeeded")
            {
                orderHeader.PaymentIntentId = paymentIntent.Id;
                orderHeader.Status = SD.Status_Approved;
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task UpdateOrderStatus(int orderId, string newStatus)
        {
            OrderHeader orderHeader = _db.OrderHeaders.First(u => u.OrderHeaderId == orderId);
         
            if (newStatus == SD.Status_Cancelled)
            {
                var options = new RefundCreateOptions
                {
                    Reason = RefundReasons.RequestedByCustomer,
                    PaymentIntent = orderHeader.PaymentIntentId
                };

                var service = new RefundService();
                Refund refund = service.Create(options);
            }
            orderHeader.Status = newStatus;
            _db.SaveChanges();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleBookStore.Data;
using SimpleBookStore.Models;
using SimpleBookStore.Models.ViewModels;
using SimpleBookStore.Service.IService;
using SimpleBookStore.Utility;
using SimpleBookStore.Utility.Helper;
using Stripe;
using Stripe.Checkout;

namespace SimpleBookStore.Service
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        public OrderService(
            AppDbContext db, 
            IHttpContextAccessor httpContextAccessor,
            IEmailService emailService)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
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

        public async Task<IEnumerable<OrderHeader>> GetOrdersHeaderAsync()
        {
            return await _db.OrderHeaders
                .OrderByDescending(o => o.OrderTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderHeader>> GetOrdersHeaderAsync(string userId)
        {
            return await _db.OrderHeaders
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderTime)
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
                    OrderTime = TimeHelper.Now(),
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
                PaymentMethodTypes = new List<string> { "card" },
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

            var email = _db.ApplicationUsers.FirstOrDefault(o => o.Id == orderHeader.UserId)?.Email;
            if (!string.IsNullOrEmpty(email))
            {
                options.CustomerEmail = email;
            }

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
                orderHeader.Status = SD.Status_Paid;
                _db.SaveChanges();

                // 查MAIL
                var user = await _db.Users
                    .FirstOrDefaultAsync(u => u.Id == orderHeader.UserId);

                await _emailService.SendEmailAsync(user.Email,
                    "SimpleBookStore - 訂單確認",
                    $@"
                    <div style='font-family: Microsoft JhengHei, Arial, sans-serif; max-width: 500px; margin: 0 auto; padding: 20px;'>
                        <div style='background: #007bff; color: white; padding: 10px; text-align: center; border-radius: 8px 8px 0 0;'>
                            <h2>SimpleBookStore</h2>
                            <h3>訂單確認</h3>
                        </div>
        
                        <div style='background: #f8f9fa; padding: 20px; border-radius: 0 0 8px 8px;'>
                            <p>親愛的 <strong>{user.FullName}</strong> 您好，</p>
                            <p>感謝您的購買！您的訂單已成功完成。</p>
            
                            <div style='background: white; padding: 15px; border-radius: 5px; margin: 15px 0; border-left: 4px solid #007bff;'>
                                <p><strong>訂單編號：</strong>#{orderHeader.OrderHeaderId:D6}</p>
                                <p><strong>訂單金額：</strong>NT$ {orderHeader.OrderTotal:N0}</p>
                                <p><strong>付款狀態：</strong><span style='color: #28a745;'>✅ 已付款</span></p>
                            </div>
            
                            <p>我們將於 1-2 個工作天內為您出貨，謝謝！</p>
            
                            <div style='text-align: center; margin: 20px 0;'>
                                <p style='color: #666; font-size: 14px;'>祝您閱讀愉快！</p>
                            </div>
                        </div>
                    </div>
                ");

                return true;
            }
            return false;
        }

        public async Task UpdateOrderStatus(int orderId, string newStatus)
        {
            OrderHeader orderHeader = _db.OrderHeaders.First(u => u.OrderHeaderId == orderId);
         
            if (newStatus == SD.Status_Cancelled && orderHeader.Status == SD.Status_Paid)
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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleBookStore.Data;
using SimpleBookStore.Models;
using SimpleBookStore.Models.ViewModels;
using SimpleBookStore.Service.IService;
using Stripe.Climate;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SimpleBookStore.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        private readonly ICouponService _couponService;
        public OrderController(
            AppDbContext db,
            IOrderService orderService,
            ICartService cartService, 
            ICouponService couponService)
        {
            _db = db;
            _orderService = orderService;
            _cartService = cartService;
            _couponService = couponService;
        }

        public async Task<IActionResult> MyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderService.GetOrdersHeaderAsync(userId);
            return View(orders);
        }

        public async Task<IActionResult> Detail(int orderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var order = await _orderService.GetOrderAsync(orderId);

            if (order == null || order.UserId != userId)
            {
                return NotFound();
            }

            return View(order);
        }

        public async Task<IActionResult> Checkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItems = await _db.ShoppingCarts
                .Where(c => c.UserId == userId)
                .Include(c => c.Product).ThenInclude(p => p.Author)
                .ToListAsync();

            if (!cartItems.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            var vm = new CheckoutVM
            {
                CartItems = cartItems,
                Total = cartItems.Sum(x => x.UnitPrice * x.Quantity)
            };

            var couponCode = Request.Cookies["CouponCode"];
            if (couponCode != null)
            {
                var coupon = await _couponService.GetAsync(couponCode);
                if (coupon != null && vm.Total > coupon.MinimumSpend)
                {
                    vm.CouponResult = new CouponResult
                    {
                        CouponCode = coupon.Code,
                        DiscountAmount = coupon.DiscountAmount,
                    };
                }
                else if (coupon == null)
                {
                    Response.Cookies.Delete("CouponCode");
                }
            }
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutVM vm)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var couponCode = Request.Cookies["CouponCode"];
            if (!ModelState.IsValid)
            {
                vm.CartItems = await _db.ShoppingCarts
                    .Where(c => c.UserId == userId)
                    .Include(c => c.Product).ThenInclude(p => p.Author)
                    .ToListAsync();
                vm.Total = vm.CartItems.Sum(x => x.UnitPrice * x.Quantity);

                if (couponCode != null)
                {
                    var coupon = await _couponService.GetAsync(couponCode);
                    if (coupon != null && vm.Total > coupon.MinimumSpend)
                    {
                        vm.CouponResult = new CouponResult
                        {
                            CouponCode = coupon.Code,
                            DiscountAmount = coupon.DiscountAmount,
                        };
                    }
                    else
                    {
                        Response.Cookies.Delete("CouponCode");
                    }
                }
                return View(vm);
            }

            try
            {
                var orderHeaderId = await _orderService.CreateOrderAsync(userId, vm.Recipient, couponCode);
                var stripeSessionUrl = await _orderService.CreateStripeSession(orderHeaderId, couponCode);
                return Redirect(stripeSessionUrl);
            }
            catch
            {
                TempData["Error"] = "結帳失敗，請稍後再試。";
                return RedirectToAction("Index", "Cart");
            }
        }
        
        public async Task<IActionResult> Confirmation(int orderId)
        {
            var result  = await _orderService.ValidateStripeSession(orderId);
            if (result)
            {
                return RedirectToAction("OrderComplete", "Order", new { orderId });
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> OrderComplete(int orderId)
        {
            var orderHeader = await _orderService.GetOrderHeaderAsync(orderId);
            return View(orderHeader);
        }
    }
}


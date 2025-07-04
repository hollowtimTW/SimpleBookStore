using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleBookStore.Models.ViewModels;
using SimpleBookStore.Service.IService;
using System.Security.Claims;

namespace SimpleBookStore.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ICouponService _couponService;
        public CartController(
            ICartService cartService,
            ICouponService couponService)
        {
            _cartService = cartService;
            _couponService = couponService;
        }
        
        public async Task<IActionResult> Index()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var couponCode = Request.Cookies["CouponCode"];
            var cartItems = _cartService.GetCart(userId);
            var vm = new ShoppingCartVM
            {
                ShoppingCart = cartItems,
                Total = cartItems.Sum(item => item.UnitPrice * item.Quantity)
            };

            if (couponCode != null)
            {
                var coupon = await _couponService.GetAsync(couponCode);
                if (coupon != null && vm.Total >= coupon.MinimumSpend)
                {
                    vm.CouponValidation = new CouponValidation
                    {
                        CouponCode = coupon.Code,
                        DiscountAmount = coupon.DiscountAmount,
                        IsValid = true,
                        Message = "優惠碼已套用！"
                    };
                }
                else if (coupon != null && vm.Total < coupon.MinimumSpend)
                {
                    vm.CouponValidation = new CouponValidation
                    {
                        CouponCode = coupon.Code,
                        DiscountAmount = coupon.DiscountAmount,
                        IsValid = false,
                        Message = $"未達最低消費金額 {coupon.MinimumSpend} 元，無法使用此優惠碼。"
                    };
                }
                else
                {
                    vm.CouponValidation = new CouponValidation
                    {
                        CouponCode = couponCode,
                        IsValid = false,
                        Message = "優惠碼無效或已過期！"
                    };
                    Response.Cookies.Delete("CouponCode");
                }
            }

            return View(vm);
        }

        [HttpPost]
        public IActionResult CouponValidation(string couponCode)
        {
            if (!string.IsNullOrEmpty(couponCode))
            {
                Response.Cookies.Append("CouponCode", couponCode.Trim(), new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddHours(1)
                });
            }
            else
            {
                Response.Cookies.Delete("CouponCode");
            }
                return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Add(int ProductId, int Quantity, string actionType)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (actionType == "add")
            {
                await _cartService.AddToCartAsync(userId, ProductId, Quantity);
                TempData["Success"] = "商品已成功加入購物車！";
                return Redirect(Request.Headers["Referer"].ToString());
            }
            else if (actionType == "buy")
            {
                await _cartService.AddToCartAsync(userId, ProductId, Quantity);
                return RedirectToAction("Index", "Cart");
            }
            return RedirectToAction("Index", "Product");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int cartId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _cartService.RemoveCartItemAsync(cartId, userId);
            return RedirectToAction("Index");
        }

        [HttpPost("ChangeQuantity")]
        public async Task<IActionResult> ChangeQuantity([FromForm] int cartId, [FromForm] int delta)
        {
            try
            {
                await _cartService.ChangeQuantityAsync(cartId, delta);
            }
            catch
            {
                TempData["Error"] = "操作失敗，請稍後嘗試";
            }
            return RedirectToAction("Index", "Cart");
        }
    }
}

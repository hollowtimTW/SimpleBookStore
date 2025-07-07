using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleBookStore.Models;
using SimpleBookStore.Service;
using SimpleBookStore.Service.IService;
using SimpleBookStore.Utility;

namespace SimpleBookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        public async Task<IActionResult> Index()
        {
            var coupons = await _couponService.GetAllAsync();
            return View(coupons);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Coupon coupon)
        {
            if (!ModelState.IsValid)
            {
                return View(coupon);
            }

            try
            {
                await _couponService.CreateAsync(coupon);

                // 先檢查 Stripe 中是否已存在相同的優惠券代碼
                var stripeService = new Stripe.CouponService();
                try
                {
                    var existingCoupon = await stripeService.GetAsync(coupon.Code);
                    if (existingCoupon != null)
                    {
                        await stripeService.DeleteAsync(coupon.Code);
                    }
                }
                catch 
                {
                }
                var options = new Stripe.CouponCreateOptions
                {
                    AmountOff = (long)(coupon.DiscountAmount * 100),
                    Name = coupon.Code,
                    Currency = "twd",
                    Id = coupon.Code,
                };

                stripeService.Create(options);
                TempData["Success"] = "操作成功！";
            }
            catch
            {
                TempData["Error"] = "發生錯誤，請稍後再試。";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ToggleActive(int id, bool? isActive)
        {
            try
            {
                await _couponService.ToggleActiveAsync(id, isActive == true);
                TempData["Success"] = "操作成功。";
            }
            catch
            {
                TempData["Error"] = "發生錯誤，請稍後再試。";
            }
            return RedirectToAction("Index");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SimpleBookStore.Models;
using SimpleBookStore.Service;
using SimpleBookStore.Service.IService;

namespace SimpleBookStore.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
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
                TempData["Success"] = "操作成功！";
            }
            catch
            {
                TempData["Error"] = "發生錯誤，請稍後再試。";
            }
            return RedirectToAction("CouponIndex", "Admin");
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
            return RedirectToAction("CouponIndex", "Admin");
        }
    }
}

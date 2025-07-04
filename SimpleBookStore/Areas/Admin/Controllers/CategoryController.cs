using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleBookStore.Models;
using SimpleBookStore.Service;
using SimpleBookStore.Service.IService;
using SimpleBookStore.Utility;

namespace SimpleBookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                TempData["Error"] = string.Join("<br/>", errors);
            }
            else
            {
                try
                {
                    await _categoryService.CreateAsync(model);
                    TempData["Success"] = "操作成功！";
                }
                catch (InvalidOperationException ex)
                {
                    TempData["Error"] = ex.Message;
                }
                catch
                {
                    TempData["Error"] = "發生錯誤，請稍後再試。";
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                TempData["Error"] = string.Join("<br/>", errors);

                return RedirectToAction("Index");
            }

            try
            {
                await _categoryService.UpdateAsync(model);
                TempData["Success"] = "操作成功！";
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
            }
            catch
            {
                TempData["Error"] = "發生錯誤，請稍後再試。";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _categoryService.DeleteAsync(id);
                TempData["Success"] = "操作成功";
            }
            catch
            {
                TempData["Error"] = "發生錯誤，請稍後再試。";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleActive(int id, bool? isActive)
        {
            try
            {
                await _categoryService.ToggleActiveAsync(id, isActive == true);
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

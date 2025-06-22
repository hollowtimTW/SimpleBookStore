using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleBookStore.Models;
using SimpleBookStore.Service.IService;
using SimpleBookStore.Utility;
using SimpleBookStore.ViewModels;
using System.Threading.Tasks;

namespace SimpleBookStore.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IAuthorService _authorService;

        public ProductController(
            IProductService productService,
            ICategoryService categoryService,
            IAuthorService authorService
            )
        {
            _productService = productService;
            _categoryService = categoryService;
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetActiveCategories();
            var authors = await _authorService.GetAllAsync();

            var viewModel = new ProductVM
            {
                Product = null,
                CategoryList = categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList(),
                AuthorList = authors.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductVM model)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetActiveCategories();
                var authors = await _authorService.GetAllAsync();

                var viewModel = new ProductVM
                {
                    Product = model.Product,
                    CategoryList = categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList(),
                    AuthorList = authors.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).ToList()
                };
                return View(viewModel);
            }

            try
            {
                await _productService.CreateAsync(model.Product);
                TempData["Success"] = "操作成功！";
                return RedirectToAction("ProductIndex", "Admin");
            }
            catch
            {
                var categories = await _categoryService.GetActiveCategories();
                var authors = await _authorService.GetAllAsync();

                var viewModel = new ProductVM
                {
                    Product = model.Product,
                    CategoryList = categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList(),
                    AuthorList = authors.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).ToList()
                };
                TempData["Error"] = "發生錯誤，請稍後再試。";
                return View(viewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var categories = await _categoryService.GetActiveCategories();
            var authors = await _authorService.GetAllAsync();

            var viewModel = new ProductVM
            {
                Product = product,
                CategoryList = categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList(),
                AuthorList = authors.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductVM model)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetActiveCategories();
                var authors = await _authorService.GetAllAsync();

                var viewModel = new ProductVM
                {
                    Product = model.Product,
                    CategoryList = categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList(),
                    AuthorList = authors.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).ToList()
                };

                return View(viewModel);
            }

            try
            {
                await _productService.UpdateAsync(model.Product);
                TempData["Success"] = "操作成功！";
                return RedirectToAction("ProductIndex", "Admin");
            }
            catch
            {
                var categories = await _categoryService.GetActiveCategories();
                var authors = await _authorService.GetAllAsync();

                var viewModel = new ProductVM
                {
                    Product = model.Product,
                    CategoryList = categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList(),
                    AuthorList = authors.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).ToList()
                };
                TempData["Error"] = "發生錯誤，請稍後再試。";
                return View(viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _productService.DeleteAsync(id);
                TempData["Success"] = "操作成功";
            }
            catch
            {
                TempData["Error"] = "發生錯誤，請稍後再試";
            }
            return RedirectToAction("ProductIndex", "Admin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleActive(int id, bool? isActive)
        {
            try
            {
                await _productService.ToggleActiveAsync(id, isActive == true);
                TempData["Success"] = "操作成功。";
            }
            catch
            {
                TempData["Error"] = "發生錯誤，請稍後再試。";
            }
            return RedirectToAction("ProductIndex", "Admin");
        }
    }
}
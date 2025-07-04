using Microsoft.AspNetCore.Mvc;
using SimpleBookStore.Service.IService;
using SimpleBookStore.Models.ViewModels;

namespace SimpleBookStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(
            IProductService productService,
            ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(
            int page = 1,
            string sort_by = "created_at",
            string order_by = "desc",
            int limit = 12)
        {
            var vm = await _productService.GetPagedProductsAsync(limit, page, sort_by, order_by);
            return View(vm);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var product = await _productService.GetAsync(id);
            return View(product);
        }

        public async Task<IActionResult> Category(int id)
        {
            var product = await _productService.GetByCategoryAsync(id);
            var category = await _categoryService.GetAsync(id);
            ViewBag.Name = category?.Name;
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Search(
             string keyword, int? categoryId, int? priceFrom, int? priceTo,
             string sortBy = "date_desc", int page = 1, int limit = 12)
        {
            var categoryList = await _categoryService.GetAllAsync();

            var (productList, totalPages, totalCount) = await _productService.SearchProductsAsync(
                keyword, categoryId, priceFrom, priceTo, sortBy, page, limit
            );

            var vm = new ProductSearchVM
            {
                Keyword = keyword,
                CategoryId = categoryId,
                CategoryList = categoryList,
                PriceFrom = priceFrom,
                PriceTo = priceTo,
                SortBy = sortBy,
                ProductList = productList,
                CurrentPage = page,
                TotalPages = totalPages,
                PageSize = limit
            };

            return View(vm);
        }
    }
}

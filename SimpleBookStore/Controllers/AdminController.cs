using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleBookStore.Service.IService;
using SimpleBookStore.Utility;
using SimpleBookStore.ViewModels;
using System.Threading.Tasks;

namespace SimpleBookStore.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IAuthorService _authorService;
        private readonly ICouponService _couponService;

        public AdminController(
            IProductService productService,
            ICategoryService categoryService,
            IAuthorService authorService,
            ICouponService couponService
            )
        {
            _productService = productService;
            _categoryService = categoryService;
            _authorService = authorService;
            _couponService = couponService;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OrderIndex()
        {
            return View();
        }

        public async Task<IActionResult> ProductIndex()
        {
            var products = await _productService.GetAllAsync();
            var categories = await _categoryService.GetActiveCategories();
            var authors = await _authorService.GetAllAsync();

            var viewModel = new ProductIndexVM
            {
                ProductList = products,
                CategoryList = categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList(),
                AuthorList = authors.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).ToList()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> CategoryIndex()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        public async Task<IActionResult> AuthorIndex()
        {
            var authors = await _authorService.GetAllAsync();
            return View(authors);
        }

        public async Task<IActionResult> CouponIndex()
        {
            var coupons = await _couponService.GetAllAsync();
            return View(coupons);
        }

        public IActionResult CustomerIndex()
        {
            return View();
        }

        public IActionResult ReviewsIndex()
        {
            return View();
        }
    }
}

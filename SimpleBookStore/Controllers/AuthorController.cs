using Microsoft.AspNetCore.Mvc;
using SimpleBookStore.Models.ViewModels;
using SimpleBookStore.Service.IService;

namespace SimpleBookStore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IProductService _productService;
        public AuthorController(
            IAuthorService authorService, 
            IProductService productService)
        {
            _authorService = authorService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var authors = await _authorService.GetAllWithProductsAsync();
            return View(authors);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var author = await _authorService.GetAsync(id);
            var products = await _productService.GetByAuthorAsync(id);

            var vm = new AuthorDetailVM
            {
                Author = author,
                Products = products
            };
            return View(vm);
        }
    }
}

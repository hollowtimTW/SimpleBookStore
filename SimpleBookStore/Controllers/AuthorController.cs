using Microsoft.AspNetCore.Mvc;
using SimpleBookStore.Models;
using SimpleBookStore.Service;
using SimpleBookStore.Service.IService;
using System.Threading.Tasks;

namespace SimpleBookStore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Author author, IFormFile ImageFile)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }

            try
            {
                await _authorService.CreateAsync(author, ImageFile);
                TempData["Success"] = "操作成功！";
            }
            catch
            {
                TempData["Error"] = "發生錯誤，請稍後再試。";
            }
            return RedirectToAction("AuthorIndex", "Admin");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var author = await _authorService.GetAsync(id); 
            return View(author);
        }
    }
}

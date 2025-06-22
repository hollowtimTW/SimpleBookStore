using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleBookStore.Models;
using SimpleBookStore.Service;
using SimpleBookStore.Service.IService;
using SimpleBookStore.Utility;
using System.Threading.Tasks;

namespace SimpleBookStore.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService; 
        private readonly ILogger<AuthorController> _logger;
        public AuthorController(IAuthorService authorService, ILogger<AuthorController> logger)
        {
            _authorService = authorService;
            _logger = logger;
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Author author, IFormFile? imageFile = null)
        {
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key]?.Errors;
                    if (errors != null && errors.Count > 0)
                    {
                        foreach (var error in errors)
                        {
                            _logger.LogWarning($"欄位 {key} 發生錯誤：{error.ErrorMessage}");
                        }
                    }
                }
                return View(author);
            }

            try
            {
                await _authorService.CreateAsync(author, imageFile);
                TempData["Success"] = "操作成功！";
                return RedirectToAction("AuthorIndex", "Admin");
            }
            catch
            {
                TempData["Error"] = "發生錯誤，請稍後再試。";
                return View(author);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var author = await _authorService.GetAsync(id); 
            return View(author);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Author author, IFormFile? imageFile = null)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }

            try
            {
                await _authorService.UpdateAsync(author, imageFile);
                TempData["Success"] = "操作成功！";
                return RedirectToAction("AuthorIndex", "Admin");
            }
            catch
            {
                TempData["Error"] = "發生錯誤，請稍後再試。";
                return View(author);
            }
        }
    }
}

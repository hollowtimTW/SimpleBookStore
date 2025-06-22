using Microsoft.AspNetCore.Mvc;

namespace SimpleBookStore.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

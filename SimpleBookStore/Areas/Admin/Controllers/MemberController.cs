using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleBookStore.Data;
using SimpleBookStore.Models.ViewModels;
using SimpleBookStore.Utility;

namespace SimpleBookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class MemberController : Controller
    {
        private readonly AppDbContext _db;
        public MemberController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var customerRoleId = _db.Roles.FirstOrDefault(r => r.Name == SD.Role_Customer)?.Id;
            var members = _db.Users
                .Where(p => _db.UserRoles.Any(ur => ur.UserId == p.Id && ur.RoleId == customerRoleId))
                .OrderBy(p => p.CreatedAt)
                .ToList();
            return View(members);
        }

        public IActionResult Detail(string id)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id);
            var orders = _db.OrderHeaders.Where(o => o.UserId == id).OrderByDescending(o => o.OrderTime).ToList();

            var vm = new MemberDetailVM
            {
                User = user,
                Orders = orders
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult ToggleBan(string id)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.IsBanned = !user.IsBanned;
                _db.SaveChanges();
            }   
            return RedirectToAction("Detail", new { id = id });
        }

        [HttpPost]
        public IActionResult ValidAccount(string id)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.EmailConfirmed = !user.EmailConfirmed;
                _db.SaveChanges();
            }
            return RedirectToAction("Detail", new { id = id });
        }

    }
}

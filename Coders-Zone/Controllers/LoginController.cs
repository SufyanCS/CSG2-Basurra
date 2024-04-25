using Microsoft.AspNetCore.Mvc;
using Coders_Zone.Data;
using Coders_Zone.Models;
using Microsoft.AspNetCore.Identity;

namespace Coders_Zone.Controllers
{
    public class LoginController : Controller
    {
        private readonly CoderZoneDbContext _Db;

        public LoginController(CoderZoneDbContext db)
        {
            _Db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Index", "Courses");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(User login)
        {
            if (string.IsNullOrEmpty(login.Username) && string.IsNullOrEmpty(login.HashedPassword))
            {
                return View(login);
            }

            if (IsBanned(login.Username))
            {
                TempData["Error"] = "Your account has been blocked. Please contact technical support";
                return View(login);
            }

            int? userId = GetUserId(login.Username, login.HashedPassword);
            if (userId.HasValue)
            {
                HttpContext.Session.SetInt32("UserId", userId.Value);
                if (IsAdmin(login.Username))
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    return RedirectToAction("Index", "Courses");
                }
            }
            else
            {
                ModelState.AddModelError("HashedPassword", "Error in Username or Password");
                return View(login);
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }



        private int? GetUserId(string username, string password)
        {
            var user = _Db.Users.FirstOrDefault(u => u.Username == username && u.HashedPassword == password);
            return user?.Id;
        }

        public bool IsBanned(string username)
        {
            var banned = _Db.Users.FirstOrDefault(b => b.Username == username);
            return banned?.IsBanned ?? false;
        }

        public bool IsAdmin(string username)
        {
            var admin = _Db.Users.FirstOrDefault(b => b.Username == username);
            return admin?.IsAdmin ?? false;
        }
    }
}
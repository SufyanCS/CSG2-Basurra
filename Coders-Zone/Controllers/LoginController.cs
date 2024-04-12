using Microsoft.AspNetCore.Mvc;
using Coders_Zone.Data;
using Coders_Zone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Coders_Zone.Controllers
{
    public class LoginController : Controller
    {
        public LoginController(CoderZoneDbContext Db) 
        {
            _Db = Db;
        }
        private readonly CoderZoneDbContext _Db;
        [HttpGet]
        public IActionResult Index()
        {
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

            if (login.IsBanned == true)
            {
                ModelState.AddModelError("HashedPassword", "Your account has been blocked.Please contact technical support");
                return View(login);
            }

           else if (IsValidUser(login.Username, login.HashedPassword) && login.IsAdmin == false) 
            
            {
                return RedirectToAction("Index", "Courses");

            }
            else if (IsValidUser(login.Username, login.HashedPassword) && login.IsAdmin == true)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ModelState.AddModelError("HashedPassword", "Error in Username Or Password");
                return View(login);
            }
            //if (!string.IsNullOrEmpty(login.Username) && !string.IsNullOrEmpty(login.HashedPassword) && IsValidUser(login.Username, login.HashedPassword) && login.IsAdmin == false)
            //{

                //        return RedirectToAction("Index", "Courses");
                //    }
                //    else if ( !string.IsNullOrEmpty(login.Username) && !string.IsNullOrEmpty(login.HashedPassword) && IsValidUser(login.Username, login.HashedPassword) && login.IsAdmin == true)
                //    {
                //        return RedirectToAction("Index", "Dashboard");

                //    }
                //    else
                //    {
                //        ModelState.AddModelError("HashedPassword", "Error in Username Or Password");
                //        return View(login);
                //    }


            }

        private bool IsValidUser(string username, string password)
        {
            var user=_Db.Users.FirstOrDefault(u=> u.Username== username && u.HashedPassword==password);
            return user!=null;
        }
    }
}

using Coders_Zone.Data;
using Coders_Zone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
namespace Coders_Zone.Controllers
{

    
    public class SignUpController : Controller
    {
        private readonly CoderZoneDbContext _Db;

        public SignUpController(CoderZoneDbContext db)
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
        public IActionResult Index(User signUp)
        {
           
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";


            //if (!ModelState.IsValid)
            //{

            //    return View(signUp);


            //}
            if (string.IsNullOrEmpty(signUp.Username) || string.IsNullOrEmpty(signUp.Email) || string.IsNullOrEmpty(signUp.HashedPassword))
            {
               // ModelState.AddModelError("", "Please fill in all the fields");
                return View(signUp);
            }

            if (!Regex.IsMatch(signUp.Email, pattern))
                {
                    ModelState.AddModelError("Email", "Incorrect e-mail format");
                    return View(signUp);

                }
                if (IsValidateUser(signUp.Username))
                {

                    ModelState.AddModelError("Username", "UserName was used before,Please enter another Username");
                    return View(signUp);

                }
           
                
                else
                {
                _Db.Users.Add(signUp);
                _Db.SaveChanges();
                return RedirectToAction("index", "login");
                 }



        }
        public bool IsValidateUser(string username)
        {
            var user = _Db.Users.FirstOrDefault(u => u.Username==username);

            return user != null;
        }
    }
}

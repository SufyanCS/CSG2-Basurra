using Coders_Zone.Data;
using Coders_Zone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

namespace Coders_Zone.Controllers
{
    public class HomeController : Controller
    {
        private readonly CoderZoneDbContext _db;

        public HomeController(CoderZoneDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            // Get the user ID from session
            var userId = HttpContext.Session.GetInt32("UserId");

            // Featured courses (first 6 courses)
            var featuredCourses = _db.Courses.Take(6).ToList();

            // If user is logged in, retrieve their enrolled courses
            var userCourses = new List<Course>();
            if (userId != null)
            {
                userCourses = _db.UserCourses
                    .Where(uc => uc.UserId == userId)
                    .Include(uc => uc.Course)
                    .Select(uc => uc.Course)
                    .ToList();
            }

            // Populate the view model
            var viewModel = new CoursesViewModel
            {
                FeaturedCourses = featuredCourses,
                UserCourses = userCourses,
            };

            return View(viewModel);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

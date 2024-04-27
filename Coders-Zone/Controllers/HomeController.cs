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
            var activeStudentsCount = _db.Users.Count(); // Assuming all users are students
            var totalCoursesCount = _db.Courses.Count();
            var totalLessonsCount = _db.Lessons.Count();
            var userId = HttpContext.Session.GetInt32("UserId");

            var featuredCourses = _db.Courses.Take(6).ToList();

            var userCourses = new List<Course>();
            if (userId != null)
            {
                userCourses = _db.UserCourses
                    .Where(uc => uc.UserId == userId)
                    .Include(uc => uc.Course)
                    .Select(uc => uc.Course)
                    .ToList();
            }

            var numEnrolledUsers = _db.UserCourses
     .GroupBy(uc => uc.CourseId)
     .Select(g => new { CourseId = g.Key, Count = g.Count() })
     .ToDictionary(x => x.CourseId, x => x.Count);

            var viewModel = new CoursesViewModel
            {
                FeaturedCourses = featuredCourses,
                UserCourses = userCourses,
                NumEnrolledUsers = numEnrolledUsers,
                ActiveStudentsCount = activeStudentsCount,
                TotalCoursesCount = totalCoursesCount,
                TotalLessonsCount = totalLessonsCount

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

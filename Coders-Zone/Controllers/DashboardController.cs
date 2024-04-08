using Coders_Zone.Data;
using Coders_Zone.Models;
using Microsoft.AspNetCore.Mvc;

namespace Coders_Zone.Controllers
{
    public class DashboardController : Controller
    {
        public DashboardController(CoderZoneDbContext db)
        {
            _db = db;
        }
        private readonly CoderZoneDbContext _db;
        public IActionResult Index()
        {
            var usersList = _db.Users.ToList();
            var coursesList = _db.Courses.ToList();
            var LessonsList = _db.Lessons.ToList();


            var viewModel = new DashboardViewModel
            {
                Users = usersList,
                Courses = coursesList,
                Lessons = LessonsList

            };

            return View(viewModel);
        }
        public IActionResult New()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Course course)
        {
            _db.Courses.Add(course);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
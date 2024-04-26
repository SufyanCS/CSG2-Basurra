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
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);

            if (userId == null || user == null || !user.IsAdmin)
            {
                return RedirectToAction("Index", "Courses");
            }

            var usersList = _db.Users.ToList();
            var coursesList = _db.Courses.ToList();
            var lessonsList = _db.Lessons.ToList();
            var viewModel = new DashboardViewModel
            {
                Users = usersList,
                Courses = coursesList,
                Lessons = lessonsList,
                UserId = userId.Value
            };

            return View(viewModel);
        }
        public IActionResult NewCourse()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);

            if (userId == null || user == null || !user.IsAdmin)
            {
                return RedirectToAction("Index", "Courses");
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewCourse(Course course)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);

            if (userId == null || user == null || !user.IsAdmin)
            {
                return RedirectToAction("Index", "Courses");
            }

            _db.Courses.Add(course);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult EditCourse(int? Id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);

            if (userId == null || user == null || !user.IsAdmin)
            {
                return RedirectToAction("Index", "Courses");
            }

            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var course = _db.Courses.Find(Id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCourse(Course course, List<Faq> FAQs)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);

            if (userId == null || user == null || !user.IsAdmin)
            {
                return RedirectToAction("Index", "Courses");
            }

            course.Faqs = FAQs;
            _db.Courses.Update(course);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCourse(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);

            if (userId == null || user == null || !user.IsAdmin)
            {
                return RedirectToAction("Index", "Courses");
            }

            var course = _db.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }

            _db.Courses.Remove(course);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult NewLesson()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);

            if (userId == null || user == null || !user.IsAdmin)
            {
                return RedirectToAction("Index", "Courses");
            }

            var courses = _db.Courses.ToList();

            ViewBag.Courses = courses;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewLesson(Lesson lesson)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);

            if (userId == null || user == null || !user.IsAdmin)
            {
                return RedirectToAction("Index", "Courses");
            }

            _db.Lessons.Add(lesson);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult EditLesson(int? Id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);

            if (userId == null || user == null || !user.IsAdmin)
            {
                return RedirectToAction("Index", "Courses");
            }

            var courses = _db.Courses.ToList();

            ViewBag.Courses = courses;
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var lesson = _db.Lessons.Find(Id);

            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditLesson(Lesson lesson)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);

            if (userId == null || user == null || !user.IsAdmin)
            {
                return RedirectToAction("Index", "Courses");
            }

            _db.Lessons.Update(lesson);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult DeleteLesson(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);

            if (userId == null || user == null || !user.IsAdmin)
            {
                return RedirectToAction("Index", "Courses");
            }

            var lesson = _db.Lessons.Find(id);
            if (lesson == null)
            {
                return NotFound();
            }

            _db.Lessons.Remove(lesson);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UnbanUser(int userId)
        {
            var user = _db.Users.Find(userId);
            if (user == null)
            {
                return NotFound();
            }

            user.IsBanned = false;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult BanUser(int userId)
        {
            var user = _db.Users.Find(userId);
            if (user == null)
            {
                return NotFound();
            }

            user.IsBanned = true;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        private bool IsUserLoggedIn()
        {
            return HttpContext.Session.GetInt32("UserId").HasValue;
        }

        // Helper method to get current user's ID
        private int GetCurrentUserId()
        {
            return HttpContext.Session.GetInt32("UserId") ?? 0;
        }


    }
}
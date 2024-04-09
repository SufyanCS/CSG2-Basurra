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
        public IActionResult NewCourse()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewCourse(Course course)
        {
            _db.Courses.Add(course);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult EditCourse(int? Id)
        {
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
        public IActionResult EditCourse(Course course)
        {
            _db.Courses.Update(course);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCourse(int id)
        {
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
            var courses = _db.Courses.ToList();

            ViewBag.Courses = courses;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewLesson(Lesson lesson)
        {
            _db.Lessons.Add(lesson);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult EditLesson(int? Id)
        {
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
            _db.Lessons.Update(lesson);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult DeleteLesson(int id)
        {
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


    }
}
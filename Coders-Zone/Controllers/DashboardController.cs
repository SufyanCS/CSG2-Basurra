using Coders_Zone.Data;
using Coders_Zone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Coders_Zone.Controllers
{
    public class DashboardController : Controller
    {
        public DashboardController(CoderZoneDbContext db, IHostingEnvironment host)
        {
            _db = db;
            _host = host;
        }
        private readonly IHostingEnvironment _host;
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
            var enrolledList = _db.UserCourses.ToList();
            var viewModel = new DashboardViewModel
            {
                Users = usersList,
                Courses = coursesList,
                Lessons = lessonsList,
                Enrolled = enrolledList,
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
            string filename = string.Empty;
            if (course.ClientImage != null)
            {
                var permittedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
                var extension = Path.GetExtension(course.ClientImage.FileName).ToLowerInvariant();
                if (permittedExtensions.Contains(extension))
                {
                    string myUpload = Path.Combine(_host.WebRootPath, "ImagesCourses");
                    filename = course.ClientImage.FileName;
                    string fullPath = Path.Combine(myUpload, filename);
                    course.ClientImage.CopyTo(new FileStream(fullPath, FileMode.Create));
                    course.CoverImage = filename;
                }
                else
                {
                    ModelState.AddModelError("ClintImage", "Invalid file type. Only the following types are allowed: .jpg, .jpeg, .png, .gif, .bmp");
                    return View(course);
                }
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
            var dbCourse = _db.Courses.FirstOrDefault(c => c.Id == course.Id);

            if (dbCourse == null)
            {
                // لم يتم العثور على الكيان
                return NotFound();
            }

            if (userId == null || user == null || !user.IsAdmin )
            {
                return RedirectToAction("Index", "Courses");
            }

            //  نقوم بتحديث الحقول الضرورية

            dbCourse.Name = course.Name;
            dbCourse.IsPublic = course.IsPublic;
            dbCourse.Category = course.Category;
            dbCourse.Duration = course.Duration;
            dbCourse.NumLessons = course.NumLessons;

            dbCourse.Overview = course.Overview;
            if (FAQs != null && FAQs.Any())
            {
                dbCourse.Faqs = FAQs;
            }
            string filename = string.Empty;
            if (course.ClientImage != null)
            {
                var permittedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
                var extension = Path.GetExtension(course.ClientImage.FileName).ToLowerInvariant();
                if (permittedExtensions.Contains(extension))
                {
                    string myUpload = Path.Combine(_host.WebRootPath, "ImagesCourses");

                    filename = course.ClientImage.FileName;
                    string fullPath = Path.Combine(myUpload, filename);
                   // course.ClientImage.CopyTo(new FileStream(fullPath, FileMode.Create));

                    //Delete the old image
                    string old = dbCourse.CoverImage;
                    string fullOld = Path.Combine(myUpload, old);
                    if (fullPath != fullOld)
                    {
                        System.IO.File.Delete(fullOld);
                    }

                    //Save the new image
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        course.ClientImage.CopyTo(stream);
                    }
                    dbCourse.CoverImage = filename;
                }
                else
                {
                    ModelState.AddModelError("ClintImage", "Invalid file type. Only the following types are allowed: .jpg, .jpeg, .png, .gif, .bmp");
                    return View(course);
                }
            }
            // course.Faqs = FAQs;
            _db.Courses.Update(dbCourse);
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
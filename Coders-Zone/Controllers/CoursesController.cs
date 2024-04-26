using Coders_Zone.Data;
using Coders_Zone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Coders_Zone.Controllers
{
    public class CoursesController : Controller
    {

        public CoursesController(CoderZoneDbContext db)

        {
            _db = db;
        }
        private readonly CoderZoneDbContext _db;
        public IActionResult Index(string search, string[] category, string[] level)
        {
            var usersList = _db.Users.ToList();

            var coursesQuery = string.IsNullOrEmpty(search)
          ? _db.Courses
          : _db.Courses.Where(c => c.Name.Contains(search));

            if (category != null && category.Length > 0)
            {
                coursesQuery = coursesQuery.Where(c => category.Contains(c.Category));
            }

            if (level != null && level.Length > 0)
            {
                coursesQuery = coursesQuery.Where(c => level.Contains(c.Level));
            }

            var coursesList = coursesQuery.ToList();
            var categories = _db.Courses.Select(c => c.Category).Distinct().ToList();

            var userId = HttpContext.Session.GetInt32("UserId");

            var viewModel = new CoursesViewModel
            {
                Courses = coursesList,
                Categories = categories,
                UserId = userId.HasValue ? userId.Value : 0,
                Users = usersList



            };

            return View(viewModel);
        }
        public IActionResult SingleCourse(int id, int? lessonId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var course = _db.Courses
                .Include(c => c.Lessons)
                .FirstOrDefault(c => c.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            if (!lessonId.HasValue && course.Lessons.Any())
            {
                lessonId = course.Lessons.First().Id;
            }

            var viewModel = new CourseDetailsViewModel
            {
                UserId = userId.HasValue ? userId.Value : 0,
                Id = course.Id,
                Name = course.Name,
                Category = course.Category,
                Lessons = course.Lessons.ToList(),
                SelectedLessonId = lessonId.HasValue ? lessonId.Value.ToString() : null,
            };

            // Save data in the UserCourse table
            if (userId.HasValue)
            {
                // Check if the user is already enrolled in the course
                var isEnrolled = _db.UserCourses
                    .Any(uc => uc.UserId == userId.Value && uc.CourseId == course.Id);

                if (!isEnrolled)
                {
                    var userCourse = new UserCourse
                    {
                        UserId = userId.Value,
                        CourseId = course.Id,
                        Completed = false
                    };

                    _db.UserCourses.Add(userCourse);
                    _db.SaveChanges();
                }
            }

            return View(viewModel);
        }
        public IActionResult CourseDetails(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            var course = _db.Courses
              .Include(c => c.Lessons)
              .Include(c => c.Faqs)
              .Include(c => c.Reviews)
              .FirstOrDefault(c => c.Id == id); 
            if (course == null)
            {
                return NotFound();
            }
            var isEnrolled = false; // Initialize isEnrolled variable

            if (userId.HasValue)
            {
                // Check if the user is already enrolled in the course
                isEnrolled = _db.UserCourses
                    .Any(uc => uc.UserId == userId.Value && uc.CourseId == course.Id);
            }

            var viewModel = new CourseDetailsViewModel
            {
                Id = course.Id,
                Name = course.Name,
                Category = course.Category,
                Overview = course.Overview,
                Level = course.Level,
                NumLessons = course.NumLessons,
                IsPublic = course.IsPublic,
                Duration = course.Duration,
                CoverImage = course.CoverImage,
                Lessons = course.Lessons.ToList(),
                Faqs = course.Faqs.ToList(),
                Reviews = course.Reviews.ToList(),
                Users = _db.Users.ToList(),
                IsEnrolled = isEnrolled // Set the IsEnrolled property





            };

            return View(viewModel);
        }

    }

}

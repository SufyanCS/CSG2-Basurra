using Coders_Zone.Data;
using Coders_Zone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Coders_Zone.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public CoursesController(CoderZoneDbContext db)

        {
            _db = db;
        }
        private readonly CoderZoneDbContext _db;
        public IActionResult Index(string search, string[] category, string[] level)
        {
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
            var viewModel = new CoursesViewModel
            {
                Courses = coursesList,
                Categories = categories

            };

            return View(viewModel);
        }
        public IActionResult SingleCourse(int id )
        {
            var Course = _db.Courses
                .Include(c => c.Lessons)// for Lodaing the realted Lessons 
                .FirstOrDefault(c => c.Id == id);
            if (Course == null)
            {
                return NotFound();

            }
            var model = new Lesson
            {
                Course = Course,
                //Lessons = Course.Lessons
            };
            return View(model);
        }
        public IActionResult CourseDetails(int id)
        {
            var course = _db.Courses
              .Include(c => c.Lessons)
              .Include(c => c.Faqs)
              .Include(c => c.Reviews)
              .FirstOrDefault(c => c.Id == id); 
            if (course == null)
            {
                return NotFound();
            }


            var viewModel = new CourseDetailsViewModel
            {
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
                Users = _db.Users.ToList() // Retrieve all users from the User table


            };

            return View(viewModel);
        }

    }

}

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
        public IActionResult CourseDetails(int id )
        {

            var tabs = new List<TabModel>
        {
            new TabModel { Title = "OverView", Content = "LearnPress is a comprehensive WordPress LMS Plugin for WordPress. This is one of the best WordPress LMS Plugins which can be used to easily create & sell courses online. You can create a course curriculum with lessons & quizzes included which is managed with an easy-to-use interface for users. Having this WordPress LMS Plugin, now you have a chance to quickly and easily create education, online school, online-course websites with no coding knowledge required. LearnPress is free and always will be, but it is still a premium high-quality WordPress Plugin that definitely helps you with making money from your WordPress Based LMS. Just try and see how amazing it is. LearnPress WordPress Online Course plugin is lightweight and super powerful with lots of Add-Ons to empower its core system.How to use WPML Add-on for LearnPress? No comments yet! You be the first to comment." },
            new TabModel { Title = "Curriculum", Content = "LearnPress is a comprehensive WordPress LMS Plugin for WordPress. This is one of the best WordPress LMS Plugins which can be used to easily create & sell courses online. You can create a course curriculum with lessons & quizzes included which is managed with an easy-to-use interface for users. Having this WordPress LMS Plugin, now you have a chance to quickly and easily create education, online school, online-course websites with no coding knowledge required. LearnPress is free and always will be, but it is still a premium high-quality WordPress Plugin that definitely helps you with making money from your WordPress Based LMS. Just try and see how amazing it is. LearnPress WordPress Online Course plugin is lightweight and super powerful with lots of Add-Ons to empower its core system.How to use WPML Add-on for LearnPress? No comments yet! You be the first to comment." },
            new TabModel { Title = "Instructor", Content = "LearnPress is a comprehensive WordPress LMS Plugin for WordPress. This is one of the best WordPress LMS Plugins which can be used to easily create & sell courses online. You can create a course curriculum with lessons & quizzes included which is managed with an easy-to-use interface for users. Having this WordPress LMS Plugin, now you have a chance to quickly and easily create education, online school, online-course websites with no coding knowledge required. LearnPress is free and always will be, but it is still a premium high-quality WordPress Plugin that definitely helps you with making money from your WordPress Based LMS. Just try and see how amazing it is. LearnPress WordPress Online Course plugin is lightweight and super powerful with lots of Add-Ons to empower its core system.How to use WPML Add-on for LearnPress? No comments yet! You be the first to comment." },
             new TabModel { Title = "FAQs", Content = "LearnPress is a comprehensive WordPress LMS Plugin for WordPress. This is one of the best WordPress LMS Plugins which can be used to easily create & sell courses online. You can create a course curriculum with lessons & quizzes included which is managed with an easy-to-use interface for users. Having this WordPress LMS Plugin, now you have a chance to quickly and easily create education, online school, online-course websites with no coding knowledge required. LearnPress is free and always will be, but it is still a premium high-quality WordPress Plugin that definitely helps you with making money from your WordPress Based LMS. Just try and see how amazing it is. LearnPress WordPress Online Course plugin is lightweight and super powerful with lots of Add-Ons to empower its core system.How to use WPML Add-on for LearnPress? No comments yet! You be the first to comment.." },
              new TabModel { Title = "Reviews", Content = "LearnPress is a comprehensive WordPress LMS Plugin for WordPress. This is one of the best WordPress LMS Plugins which can be used to easily create & sell courses online. You can create a course curriculum with lessons & quizzes included which is managed with an easy-to-use interface for users. Having this WordPress LMS Plugin, now you have a chance to quickly and easily create education, online school, online-course websites with no coding knowledge required. LearnPress is free and always will be, but it is still a premium high-quality WordPress Plugin that definitely helps you with making money from your WordPress Based LMS. Just try and see how amazing it is. LearnPress WordPress Online Course plugin is lightweight and super powerful with lots of Add-Ons to empower its core system.How to use WPML Add-on for LearnPress? No comments yet! You be the first to comment.." },
              //new TabModel{Title="Curriculum",Content="this is content ", FrameContent="but this is the paragraph inside the content  "}
         
              
        }
            ;

            return View(tabs);
        }
    }

}

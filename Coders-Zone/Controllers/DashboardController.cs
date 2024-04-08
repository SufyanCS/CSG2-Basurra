﻿using Coders_Zone.Data;
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


        public IActionResult Edit(int? Id)
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
        public IActionResult Edit(Course course)
        {
            _db.Courses.Update(course);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
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

    }
}
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Coders_Zone.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Level { get; set; }
        public string Overview { get; set; }

        public int NumLessons { get; set; }
        public bool IsPublic { get; set; }
        public string Duration { get; set; }
        public string CoverImage { get; set; }
        public ICollection<UserCourse> UserCourses { get; set; } // Navigation property for UserCourses
        public ICollection<Lesson> Lessons { get; set; } // Navigation property for Lessons
        public ICollection<Faq> Faqs { get; set; } // Navigation property for Faqs
        public ICollection<Review> Reviews { get; set; } // Navigation property for Reviews
    }
}

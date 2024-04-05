using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Coders_Zone.Models
{
    public class User
    {
        //public string UserName { get; internal set; }
        public int Id { get; set; } // Auto-incrementing ID
        public string Username { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; } // Hashed password
        public DateTime RegistrationDate { get; set; }
        public bool IsAdmin { get; set; }

        public ICollection<UserCourse> UserCourses { get; set; } // Navigation property for UserCourses
        public ICollection<Review> Reviews { get; set; } // Navigation property for Reviews
        public ICollection<UserLesson> UserLessons { get; set; }

    }
}
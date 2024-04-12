using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Coders_Zone.Models
{
    public class User
    {
        //public string UserName { get; internal set; }
        [Key]
        public int Id { get; set; }
        // Auto-incrementing ID
        [Required]
        [DisplayName("User Name")]
      
        public string Username { get; set; }
        [Required]
        
        public string Email { get; set; }
        [Required]
        [DisplayName("Password")]
        public string HashedPassword { get; set; } // Hashed password
        public DateTime RegistrationDate { get; set; }= DateTime.Now;
        public bool IsAdmin { get; set; }= false;
        public bool IsBanned { get; set; } = false; // New property to indicate whether the user is banned or not
        public ICollection<UserCourse> UserCourses { get; set; } // Navigation property for UserCourses
        public ICollection<Review> Reviews { get; set; } // Navigation property for Reviews
        public ICollection<UserLesson> UserLessons { get; set; }

    }
}
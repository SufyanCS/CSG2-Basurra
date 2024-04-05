namespace Coders_Zone.Models
{
    public class UserCourse
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } // Navigation property for User

        public int CourseId { get; set; }
        public Course Course { get; set; } // Navigation property for Course

        public bool Completed { get; set; }
    }
}

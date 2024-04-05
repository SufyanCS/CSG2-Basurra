namespace Coders_Zone.Models
{
    public class UserLesson
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } // Navigation property for User

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; } // Navigation property for Lesson

        public bool Completed { get; set; }
    }
}

namespace Coders_Zone.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; } // Navigation property for Course

        public string Content { get; set; }
        public int Order { get; set; }
        public string Duration { get; set; }
        public string VideoUrl { get; set; }
        public bool IsPreview { get; set; }

        public ICollection<UserLesson> UserLessons { get; set; } // Navigation property for UserLessons
        //public ICollection<Lesson> Lessons { get; set; }
    }
}

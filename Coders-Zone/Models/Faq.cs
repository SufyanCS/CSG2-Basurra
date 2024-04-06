namespace Coders_Zone.Models
{
    public class Faq
    {
        public int Id { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; } // Navigation property for Course

        public string Question { get; set; }
        public string Answer { get; set; }
        public int Order { get; set; }
    }
}

namespace Coders_Zone.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; } // Navigation property for Course

        public int UserId { get; set; }
        public User User { get; set; } // Navigation property for User

        public double Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

namespace Coders_Zone.Models
{
    public class CourseDetailsViewModel
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
        public List<Lesson> Lessons { get; set; }
        public List<Faq> Faqs { get; set; }
        public List<Review> Reviews { get; set; }
        public List<User> Users { get; set; } // Add this property for Users





    }
}

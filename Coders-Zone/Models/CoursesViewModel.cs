namespace Coders_Zone.Models
{
    public class CoursesViewModel
    {
        public List<Course> Courses { get; set; }
        public List<string> Categories { get; set; }
        public Int32 UserId { get; set; }
        public List<User> Users { get; set; }
        public List<Course> FeaturedCourses { get; set; }
        public List<Course> UserCourses { get; set; }
        public Dictionary<int, int> NumEnrolledUsers { get; set; }
        public int ActiveStudentsCount { get; set; }
        public int TotalCoursesCount { get; set; }
        public int TotalLessonsCount { get; set; }



    }
}

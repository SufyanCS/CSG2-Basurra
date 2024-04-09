using System.Collections.Generic;

namespace Coders_Zone.Models
{
    public class DashboardViewModel
    {
        public List<User> Users { get; set; }
        public List<Course> Courses { get; set; }
        public List<Lesson> Lessons { get; set; }

    }
}

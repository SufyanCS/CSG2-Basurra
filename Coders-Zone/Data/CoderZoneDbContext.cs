using Coders_Zone.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Coders_Zone.Data
{
    public class CoderZoneDbContext:Microsoft.EntityFrameworkCore.DbContext
    {
        public CoderZoneDbContext(DbContextOptions<CoderZoneDbContext> option) : base(option)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<UserLesson> UserLessons { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<Review> Reviews { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserCourse>()
                .HasKey(uc => new { uc.UserId, uc.CourseId }); // Composite primary key

            modelBuilder.Entity<UserCourse>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserCourses) // One user can have many user courses
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<UserCourse>()
                .HasOne(uc => uc.Course)
                .WithMany(c => c.UserCourses) // One course can have many user courses
                .HasForeignKey(uc => uc.CourseId);

            modelBuilder.Entity<UserLesson>()
                .HasKey(ul => new { ul.UserId, ul.LessonId }); // Composite primary key

            modelBuilder.Entity<UserLesson>()
                .HasOne(ul => ul.User)
                .WithMany(u => u.UserLessons) // One user can have many user lessons
                .HasForeignKey(ul => ul.UserId);

            modelBuilder.Entity<UserLesson>()
                .HasOne(ul => ul.Lesson)
                .WithMany(l => l.UserLessons) // One lesson can have many user lessons
                .HasForeignKey(ul => ul.LessonId);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Lessons) // One course can have many lessons
                .WithOne(l => l.Course) // One lesson belongs to one course
                .HasForeignKey(l => l.CourseId);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Faqs) // One course can have many FAQs
                .WithOne(f => f.Course) // One FAQ belongs to one course
                .HasForeignKey(f => f.CourseId);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Reviews) // One course can have many reviews
                .WithOne(r => r.Course) // One review belongs to one course
                .HasForeignKey(r => r.CourseId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Reviews) // One user can have many reviews
                .WithOne(r => r.User) // One review belongs to one user
                .HasForeignKey(r => r.UserId);
        }
    }
}

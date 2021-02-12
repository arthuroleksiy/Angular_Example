using Microsoft.EntityFrameworkCore;
using Students_Angular_App.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Students_Angular_App.DAL.Context
{
    public class StudentsAppContext : DbContext
    {
        public StudentsAppContext(DbContextOptions<StudentsAppContext> options)
           : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // использование Fluent API

            modelBuilder
                .Entity<User>()
                .HasOne(u => u.Student)
                .WithOne(p => p.User)
                .HasForeignKey<Student>(p => p.UserId);

            modelBuilder.Entity<StudentCourse>()
            .HasKey(t => new { t.StudentId, t.CourseId });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId);
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<StudentInformation> StudentInformations { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }

        public DbSet<Message> Messages { get; set; }
    }
}
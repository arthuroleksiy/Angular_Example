using Students_Angular_App.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Students_Angular_App.DAL.Models
{
    public class Course
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }

        public ICollection<Student> Students { get; set; }

        public List<StudentCourse> StudentCourses { get; set; }

        List<Message> Messages { get; set; }

        public Course()
        {
            StudentCourses = new List<StudentCourse>();
            Messages = new List<Message>();
        }
    }
}

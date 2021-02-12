using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Students_Angular_App.DAL.Models
{
    public class Student
    {
        [Key]
        public long StudentId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        public DateTime BirthDate { get; set; }

        [ForeignKey("UserId")]
        public long? UserId { get; set; }
        public User User { get; set; }

        public StudentInformation StudentInformation { get; set; }

        public List<StudentCourse> StudentCourses { get; set; }

        public Student()
        {
            StudentCourses = new List<StudentCourse>();
        }
    }
}

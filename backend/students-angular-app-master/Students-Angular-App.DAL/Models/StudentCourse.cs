using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Students_Angular_App.DAL.Models
{
    public class StudentCourse
    {
        [Key]
        public long Id { get; set; }

        public long StudentId { get; set; }

        public Student Student { get; set; }

        public long CourseId { get; set; }

        public Course Course { get; set; }
    }
}

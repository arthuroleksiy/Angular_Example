using Students_Angular_App.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Students_Angular_App.DAL.Models
{
    public class StudentInformation
    {
        [System.ComponentModel.DataAnnotations.Key]
        public long InformationId { get; set; }

        public string Information { get; set; }

        public string Degree { get; set; }
        [ForeignKey("StudentId")]
        public long StudentId { get; set; }

        public Student Student { get; set; }       

    }
}

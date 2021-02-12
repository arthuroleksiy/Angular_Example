using System;
using System.Collections.Generic;
using System.Text;

namespace Student_Angular_App.Common.Dtos
{
    public class StudentDto
    {               
        public long StudentId { get; set; }       
        public string Name { get; set; }       
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }    
    }
}

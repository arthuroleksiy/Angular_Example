using Student_Angular_App.Common.Dtos;
using Students_Angular_App.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Student_Angular_App.BLL.Mappers.Realizations
{
    public class StudentMapper : IMapper<Student, StudentDto>
    {
        public Student Map(StudentDto item)
        {
            return new Student { BirthDate = item.BirthDate, Name = item.Name, Surname = item.Surname , StudentId = item.StudentId };
        }

        public StudentDto Map(Student item)
        {
            return new StudentDto { BirthDate = item.BirthDate, Name = item.Name, Surname = item.Surname , StudentId = item.StudentId };
        }

        public List<Student> MapList(List<StudentDto> dtos)
        {
            var students = new List<Student>();
            foreach(var dto in dtos)
            {
                students.Add(Map(dto));
            }
            return students;
        }

        public List<StudentDto> MapList(List<Student> entities)
        {
            var students = new List<StudentDto>();
            foreach (var entity in entities)
            {
                students.Add(Map(entity));
            }
            return students;
        }
    }
}

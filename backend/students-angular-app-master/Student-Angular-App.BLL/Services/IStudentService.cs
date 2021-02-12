using Student_Angular_App.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Student_Angular_App.BLL.Services
{
    public interface IStudentService
    {
        Task<List<StudentDto>> GetAllStudentsAsync();

        Task<List<StudentDto>> GetStudentsByName(string name);

        Task AddStudent(StudentDto studentDto);

        Task UpdateStudent(StudentDto studentDto);

        Task DeleteStudent(long studentId);
    }
}


using Student_Angular_App.BLL.Mappers;
using Student_Angular_App.Common.Dtos;
using Students_Angular_App.DAL.Models;
using Students_Angular_App.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Student_Angular_App.BLL.Services.Realizations
{
    public class StudentService : IStudentService
    {
        IRepository<Student> _studentRepository;
        IMapper<Student, StudentDto> _studentMapper;
        public StudentService(IRepository<Student> studentRepository, IMapper<Student, StudentDto> studentMapper)
        {
            _studentRepository = studentRepository;
            _studentMapper = studentMapper;
        }
        public async Task AddStudent(StudentDto studentDto)
        {
            await _studentRepository.CreateAsync(_studentMapper.Map(studentDto));
        }

        public async Task DeleteStudent(long studentId)
        {
            await _studentRepository.DeleteAsync(s => s.StudentId == studentId);
        }

        public async Task<List<StudentDto>> GetAllStudentsAsync()
        {            
            return _studentMapper.MapList(await _studentRepository.GetAllAsync());
        }

        public async Task<List<StudentDto>> GetStudentsByName(string name)
        {
            return _studentMapper.MapList(await _studentRepository.GetAllByAsync(s => s.Name.Contains(name)));
        }

        public async Task UpdateStudent(StudentDto studentDto)
        {
            await _studentRepository.UpdateAsync(_studentMapper.Map(studentDto));
        }
    }
}

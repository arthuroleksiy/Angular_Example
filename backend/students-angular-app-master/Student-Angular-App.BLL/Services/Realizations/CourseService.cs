using Students_Angular_App.Common.Dtos;
using Students_Angular_App.DAL.Models;
using Students_Angular_App.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Student_Angular_App.BLL.Services.Realizations
{
    public class CourseService : ICourseService
    {
        IRepository<Student> _studentRepository;
        IRepository<Course> _courseRepository;
        IRepository<StudentCourse> _studentCourseRepository;

        public CourseService(IRepository<Student> studentRepository,
            IRepository<Course> courseRepository,
            IRepository<StudentCourse> studentCourseRepository
            )
        {
            _studentCourseRepository = studentCourseRepository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }
        public async Task AddCourseAsync(AddCourseDto addCourseDto)
        {
            await _courseRepository.CreateAsync(new Course { Name = addCourseDto.Name, Description = addCourseDto.Description });
        }

        public async Task AddStudentCourseAsync(AddDeleteStudentCourseDto addStudentCourseDto)
        {
            await _studentCourseRepository.CreateAsync(new StudentCourse { CourseId = addStudentCourseDto.CourseId, StudentId = addStudentCourseDto.StudentId });
        }

        public async Task DeleteCourseAsync(long courseId)
        {
            await _courseRepository.DeleteAsync(c => c.Id == courseId);
        }

        public async Task DeleteStudentCourseAsync(AddDeleteStudentCourseDto addStudentCourseDto)
        {
            await _studentCourseRepository.DeleteAsync(sc => sc.CourseId == addStudentCourseDto.CourseId && sc.StudentId == addStudentCourseDto.StudentId);
        }

        public async Task<List<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            var coursesDto = new List<CourseDto>();
            foreach (var course in courses)
            {
                coursesDto.Add(new CourseDto
                {
                    Id = course.Id,
                    Description = course.Description,
                    Name = course.Name
                });
            }

            return coursesDto;
        }

        public async Task<CourseDto> GetCourse(long courseId)
        {

            var course =  await _courseRepository.GetByAsync(c => c.Id == courseId);
            return new CourseDto { Id = courseId, Description = course.Description, Name = course.Name };
        }

        public async Task<List<StudCourseDto>> GetStudentCoursesAsync(long studentId)
        {
            var coursesDtos = new List<StudCourseDto>();
            var courses = await _courseRepository.GetAllAsync();
            foreach(var course in courses)
            {
                var studentCourse = await _studentCourseRepository.GetByAsync(sc => sc.CourseId == course.Id && sc.StudentId == studentId);
                coursesDtos.Add(new StudCourseDto
                {
                    Id = course.Id,
                    Description = course.Description,
                    Name = course.Name,
                    IsSubscribed = studentCourse != null
                });
            }
            

            return coursesDtos;
        }


    }
}

using Students_Angular_App.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Student_Angular_App.BLL.Services
{
    public interface ICourseService
    {
        Task AddCourseAsync(AddCourseDto addCourseDto);

        Task AddStudentCourseAsync(AddDeleteStudentCourseDto addStudentCourseDto);
        Task DeleteStudentCourseAsync(AddDeleteStudentCourseDto addStudentCourseDto);

        Task<List<CourseDto>>  GetAllCoursesAsync();

        Task DeleteCourseAsync(long courseId);

        Task<List<StudCourseDto>> GetStudentCoursesAsync(long userId);

        Task<CourseDto> GetCourse(long courseId);
    }
}

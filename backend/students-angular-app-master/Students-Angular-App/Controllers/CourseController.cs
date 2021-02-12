using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_Angular_App.BLL.Services;
using Students_Angular_App.Common.Constants;
using Students_Angular_App.Common.Dtos;

namespace Students_Angular_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public async Task<List<CourseDto>> GetCoursesAsync()
        {
            try
            {
                return await _courseService.GetAllCoursesAsync();
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        [Authorize(Roles = Roles.Student)]
        [HttpGet("getStudentCourses")]
        public async Task<List<StudCourseDto>> GetCourseByStudIdAsync([FromQuery] long studentId)
        {
            try
            {
                return await _courseService.GetStudentCoursesAsync(studentId);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }


        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task AddCourseAsync([FromBody] AddCourseDto addCourseDto)
        {
            try
            {
                await _courseService.AddCourseAsync(addCourseDto);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        [Authorize(Roles = Roles.Student)]
        [HttpPost("addStudentCourse")]
        public async Task AddStudentCourseAsync([FromBody] AddDeleteStudentCourseDto studentCourseDto)
        {
            try
            {
                await _courseService.AddStudentCourseAsync(studentCourseDto);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        [Authorize(Roles = Roles.Student)]
        [HttpDelete("deleteStudentCourse")]
        public async Task DeleteStudentCourseAsync([FromQuery] AddDeleteStudentCourseDto studentCourseDto)
        {
            try
            {
                await _courseService.DeleteStudentCourseAsync(studentCourseDto);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpDelete]
        public async Task DeleteCourseAsync([FromQuery] long courseId)
        {
            try
            {
                await _courseService.DeleteCourseAsync(courseId);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        [Authorize]
        [HttpGet("getCourse")]
        public async Task<CourseDto> GetCourse([FromQuery] long courseId)
        {
            try
            {
               return await _courseService.GetCourse(courseId);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }


    }
}

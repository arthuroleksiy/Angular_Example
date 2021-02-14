using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student_Angular_App.BLL.Services;
using Student_Angular_App.BLL.Services.Realizations;
using Student_Angular_App.Common.Dtos;
using Students_Angular_App.Common.Constants;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Students_Angular_App.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        IStudentService _studentService;
        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        // GET: api/<StudentsController>
        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public async Task<IEnumerable<StudentDto>> Get()
        {
            var stud = await _studentService.GetAllStudentsAsync();
            return await _studentService.GetAllStudentsAsync();
        }

        // GET api/<StudentsController>/5
        [Authorize(Roles = Roles.Admin)]
        [HttpGet("{name}")]
        public async Task<IEnumerable<StudentDto>> Get(string name)
        {
            var currentUserId = int.Parse(User.Identity.Name);

            try
            {
                return await _studentService.GetStudentsByName(name);
            }
            catch(Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        // POST api/<StudentsController>
        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task AddStudent([FromBody] StudentDto student)
        {
            try
            {
                await _studentService.AddStudent(student);
            }
            catch(Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        // PUT api/<StudentsController>/5
        [HttpPut]
        public async Task UpdateStudent(StudentDto student)
        {
            try
            {
                await _studentService.UpdateStudent(student);
            }
            catch(Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        // DELETE api/<StudentsController>/5
        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{studentId}")]
        public async Task Delete(long studentId)
        {
            try
            {
                await _studentService.DeleteStudent(studentId);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }
    }
}

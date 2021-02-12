using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student_Angular_App.BLL.Services;
using Students_Angular_App.Common.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Students_Angular_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        // GET: api/<AuthenticationController>
        [HttpGet("GetStudentId")]
        [AllowAnonymous]
        public async Task<long> GetStudentId([FromQuery] StudCheckDto studCheck)
        {
            try
            {
                return await _authenticationService.GetStudentId(studCheck);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }


        // GET api/<AuthenticationController>/5
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task Register([FromBody] UserRegisterDto userRegister)
        {
            try
            {
                await _authenticationService.RegisterAsync(userRegister);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        //// POST api/<AuthenticationController>
        [AllowAnonymous]
        [HttpPost]
        public async Task<UserLoggedDto> Login(UserAuthDto userAuthDto)
        {
            try
            {
                return await _authenticationService.Authenticate(userAuthDto);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

    }
}

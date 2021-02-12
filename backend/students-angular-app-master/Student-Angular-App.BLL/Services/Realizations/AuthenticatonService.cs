using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Student_Angular_App.Common.Dtos;
using Students_Angular_App.Common.Constants;
using Students_Angular_App.Common.Dtos;
using Students_Angular_App.Common.Helpers;
using Students_Angular_App.DAL.Models;
using Students_Angular_App.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Student_Angular_App.BLL.Services.Realizations
{
    public class AuthenticatonService : IAuthenticationService
    {

        IRepository<User> _userRepository;
        IRepository<Student> _studentRepository;
        private readonly AppSettings _appSettings;
        public AuthenticatonService(IRepository<User> userRepository, IRepository<Student> studentRepository, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _appSettings = appSettings.Value;
        }

        public async Task<UserLoggedDto> Authenticate(UserAuthDto userAuthDto)
        {
            var user = await _userRepository.GetByAsync(u => u.Login == userAuthDto.Login && u.Password == userAuthDto.Password);
            if (user == null)
                throw new Exception("Login or password are invalid. Try again");

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var student = await _studentRepository.GetByAsync(s => s.UserId == user.Id);
            
            UserLoggedDto userLoggedDto = new UserLoggedDto {
                UserId = user.Id,
                Login = user.Login,
                Role = user.Role, 
                Token = tokenHandler.WriteToken(token),
                StudentId = student?.StudentId
            };

            return userLoggedDto;

        }

        public async Task RegisterAsync(UserRegisterDto userRegister)
        {
            var student = await _studentRepository.GetByAsync(st => st.StudentId == userRegister.StudentId);
            if(student == null)
            {
                throw new Exception("There is no such student in database");
            }
            if (student.UserId != null)
            {
                throw new Exception("This student has user");
            }

            var user = await _userRepository.GetByAsync(u => u.Login == userRegister.Login);
            if(user != null)
            {
                throw new Exception("User " + user.Login + " exist in database");
            }

            await _userRepository.CreateAsync(new User
            {
                Login = userRegister.Login,
                Password = userRegister.Password,
                Role = Roles.Student
            });
            var createdUser = await _userRepository.GetByAsync(u => u.Login == userRegister.Login);
            student.UserId = createdUser.Id;
            await _studentRepository.UpdateAsync(student);
        }

        public async Task<long> GetStudentId(StudCheckDto studCheckDto)
        {
            var student = await _studentRepository.GetByAsync(st => st.Name == studCheckDto.Name && st.Surname == studCheckDto.Surname); 
            if(student == null)
            {
                throw new Exception("There is no user with this name surname");
            }
            return student.StudentId;
        }
    }
}

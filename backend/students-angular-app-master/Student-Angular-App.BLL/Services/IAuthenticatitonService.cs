using Student_Angular_App.Common.Dtos;
using Students_Angular_App.Common.Dtos;
using System.Threading.Tasks;

namespace Student_Angular_App.BLL.Services
{
    public interface IAuthenticationService
    {
        Task RegisterAsync(UserRegisterDto userRegister);

        Task<UserLoggedDto> Authenticate(UserAuthDto userAuthDto);

        Task<long> GetStudentId(StudCheckDto studCheckDto);

    }
}

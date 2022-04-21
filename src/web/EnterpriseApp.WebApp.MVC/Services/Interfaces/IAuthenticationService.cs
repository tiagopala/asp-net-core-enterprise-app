using EnterpriseApp.WebApp.MVC.Models;
using System.Threading.Tasks;

namespace EnterpriseApp.WebApp.MVC.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserLoginResponse> Login(UserLoginDTO user);
        Task<UserLoginResponse> Register(UserRegisterDTO user);
        Task Login(UserLoginResponse user);
        Task Logout();
        bool IsExpiredToken();
        Task<bool> IsValidRefreshToken();
    }
}
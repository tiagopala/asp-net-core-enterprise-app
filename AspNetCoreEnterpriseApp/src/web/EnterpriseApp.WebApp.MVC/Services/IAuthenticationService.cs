using EnterpriseApp.WebApp.MVC.Models;
using System.Threading.Tasks;

namespace EnterpriseApp.WebApp.MVC.Services
{
    public interface IAuthenticationService
    {
        Task<UserLoginResponse> Login(UserLoginDTO user);
        Task<UserLoginResponse> Register(UserRegisterDTO user);
    }
}
using EnterpriseApp.WebApp.MVC.Models;
using EnterpriseApp.WebApp.MVC.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EnterpriseApp.WebApp.MVC.Services
{
    public class AuthenticationService : MainService, IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserLoginResponse> Login(UserLoginDTO user)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/auth/login", user);

            if (!response.IsSuccessStatusCode)
                await HandleErrorsResponse(response);

            return await DeserializeResponseMessage<UserLoginResponse>(response);
        }

        public async Task<UserLoginResponse> Register(UserRegisterDTO user)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/auth/register", user);

            if (!response.IsSuccessStatusCode)
                await HandleErrorsResponse(response);

            return await DeserializeResponseMessage<UserLoginResponse>(response);
        }
    }
}

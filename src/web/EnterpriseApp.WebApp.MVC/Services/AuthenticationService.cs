using EnterpriseApp.WebApp.MVC.Models;
using EnterpriseApp.WebApp.MVC.Services.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace EnterpriseApp.WebApp.MVC.Services
{
    public class AuthenticationService : MainService, IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("auth");
        }

        public async Task<UserLoginResponse> Login(UserLoginDTO user)
        {
            var loginContent = GetContent(user);

            var response = await _httpClient.PostAsync("/api/auth/login", loginContent);

            if (!response.IsSuccessStatusCode)
                await HandleErrorsResponse(response);

            return await DeserializeResponseMessage<UserLoginResponse>(response);
        }

        public async Task<UserLoginResponse> Register(UserRegisterDTO user)
        {
            var registerContent = GetContent(user);

            var response = await _httpClient.PostAsync("/api/auth/register", registerContent);

            if (!response.IsSuccessStatusCode)
                await HandleErrorsResponse(response);

            return await DeserializeResponseMessage<UserLoginResponse>(response);
        }
    }
}

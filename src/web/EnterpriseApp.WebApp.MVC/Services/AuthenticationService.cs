using EnterpriseApp.WebApp.MVC.Configuration;
using EnterpriseApp.WebApp.MVC.Models;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EnterpriseApp.WebApp.MVC.Services
{
    public class AuthenticationService : MainService, IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(
            HttpClient httpClient,
            IOptions<AuthAPIConfig> authAPIConfig)
        {
            httpClient.BaseAddress = new Uri(authAPIConfig.Value.Endpoint);
            _httpClient = httpClient;
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

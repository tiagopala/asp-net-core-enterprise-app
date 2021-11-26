using EnterpriseApp.Core.Responses;
using EnterpriseApp.WebApp.MVC.Configuration;
using EnterpriseApp.WebApp.MVC.Exceptions;
using EnterpriseApp.WebApp.MVC.Models;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EnterpriseApp.WebApp.MVC.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthAPIConfig _authAPIConfig;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public AuthenticationService(
            HttpClient httpClient,
            IOptions<AuthAPIConfig> authAPIConfig)
        {
            _httpClient = httpClient;
            _authAPIConfig = authAPIConfig.Value;

            _jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        }

        public async Task<UserLoginResponse> Login(UserLoginDTO user)
        {
            var loginContent = new StringContent(JsonSerializer.Serialize(user),Encoding.UTF8,"application/json");

            var response = await _httpClient.PostAsync($"{_authAPIConfig.Endpoint}/api/auth/login", loginContent);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var payload = JsonSerializer.Deserialize<ErrorApiResponse>(content, _jsonSerializerOptions);
                var messages = payload.Errors.Messages;
                throw new AuthException(messages);
            }

            return JsonSerializer.Deserialize<UserLoginResponse>(await response.Content.ReadAsStringAsync(), _jsonSerializerOptions);
        }

        public async Task<UserLoginResponse> Register(UserRegisterDTO user)
        {
            var registerContent = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_authAPIConfig.Endpoint}/api/auth/register", registerContent);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var payload = JsonSerializer.Deserialize<ErrorApiResponse>(content, _jsonSerializerOptions);
                var messages = payload.Errors.Messages;
                throw new AuthException(messages);
            }

            return JsonSerializer.Deserialize<UserLoginResponse>(await response.Content.ReadAsStringAsync(), _jsonSerializerOptions);
        }
    }
}

using EnterpriseApp.Core.Services.Interfaces;
using EnterpriseApp.WebApp.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using MvcInterfaces = EnterpriseApp.WebApp.MVC.Services.Interfaces;

namespace EnterpriseApp.WebApp.MVC.Services
{
    public class AuthenticationService : MainService, MvcInterfaces.IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationService(
            HttpClient httpClient,
            IUserService userService,
            IAuthenticationService authenticationService)
        {
            _httpClient = httpClient;
            _authenticationService = authenticationService;
            _userService = userService;
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

        public async Task<UserLoginResponse> RefreshToken(string refreshToken)
        {
            var refreshTokenContext = GetContent(refreshToken);

            var response = await _httpClient.PostAsync("/api/auth/refresh-token", refreshTokenContext);

            if (!response.IsSuccessStatusCode)
                await HandleErrorsResponse(response);

            return await DeserializeResponseMessage<UserLoginResponse>(response);
        }

        public async Task Login(UserLoginResponse user)
        {
            var token = GetToken(user.AccessToken);

            var claims = new List<Claim>
            {
                new Claim("jwt", user.AccessToken),
                new Claim("refreshToken", user.RefreshToken)
            };

            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8),
                IsPersistent = true
            };

            await _authenticationService.SignInAsync(_userService.GetHttpContext(), CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }

        public async Task Logout()
            => await _authenticationService.SignOutAsync(_userService.GetHttpContext(), CookieAuthenticationDefaults.AuthenticationScheme, null);

        public bool IsExpiredToken()
        {
            var jwt = _userService.GetUserToken();

            if (jwt is null)
                return false;

            var token = GetToken(jwt);
            return token.ValidTo.ToLocalTime() < DateTime.Now;
        }

        public async Task<bool> IsValidRefreshToken()
        {
            var response = await RefreshToken(_userService.GetRefreshToken());

            if(response.AccessToken is not null && response.ResponseResult is null)
            {
                await Login(response);
                return true;
            }

            return false;
        }

        private static JwtSecurityToken GetToken(string jwt)
            => new JwtSecurityTokenHandler().ReadToken(jwt) as JwtSecurityToken;
    }
}

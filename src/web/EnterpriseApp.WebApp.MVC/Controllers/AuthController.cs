using EnterpriseApp.WebApp.MVC.Exceptions;
using EnterpriseApp.WebApp.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using IAuthenticationService = EnterpriseApp.WebApp.MVC.Services.IAuthenticationService;

namespace EnterpriseApp.WebApp.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        [Route("new-account")]
        public IActionResult Register()
            => View();

        [HttpPost]
        [Route("new-account")]
        public async Task<IActionResult> Register(UserRegisterDTO user)
        {
            if (!ModelState.IsValid)
                return View(user);

            try
            {
                var response = await _authenticationService.Register(user);
                await Login(response);
            }
            catch (AuthException e)
            {
                AddErrorsToModelState(e.Message);
                return View(user);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginDTO user, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
                return View(user);

            try
            {
                var response = await _authenticationService.Login(user);
                await Login(response);
            }
            catch (AuthException e)
            {
                AddErrorsToModelState(e.Message);
                return View(user);
            }

            if(string.IsNullOrEmpty(returnUrl))
                return RedirectToAction("Index", "Home");

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private async Task Login(UserLoginResponse user)
        {
            var token = GetToken(user.AccessToken);

            var claims = new List<Claim>
            {
                new Claim("jwt", user.AccessToken)
            };

            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity),authProperties);
        }

        private static JwtSecurityToken GetToken(string jwt)
        {
            return new JwtSecurityTokenHandler().ReadToken(jwt) as JwtSecurityToken;
        }

        private void AddErrorsToModelState(string errors)
        {
            var errorsCollection = errors.Split("\n");
            for (int i = 0; i < errorsCollection.Length; i++)
            {
                ModelState.AddModelError(string.Empty, errorsCollection[i]);
            }
        }
    }
}

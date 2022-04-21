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
using IAuthenticationService = EnterpriseApp.WebApp.MVC.Services.Interfaces.IAuthenticationService;

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
                await _authenticationService.Login(response);
            }
            catch (AuthException e)
            {
                AddErrorsToModelState(e.Message);
                return View(user);
            }

            return RedirectToAction("Index", "Catalog");
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
                await _authenticationService.Login(response);
            }
            catch (AuthException e)
            {
                AddErrorsToModelState(e.Message);
                return View(user);
            }

            if(string.IsNullOrEmpty(returnUrl))
                return RedirectToAction("Index", "Catalog");

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authenticationService.Logout();
            return RedirectToAction("Index", "Catalog");
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

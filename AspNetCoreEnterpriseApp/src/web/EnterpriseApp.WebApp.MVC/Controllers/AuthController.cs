using EnterpriseApp.WebApp.MVC.Exceptions;
using EnterpriseApp.WebApp.MVC.Models;
using EnterpriseApp.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            }
            catch (AuthException e)
            {
                var exception = e.Message;
                return View(user);
            }

            if (false)
                return View(user);

            // API - Realizar login

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
            => View();

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginDTO user)
        {
            if (!ModelState.IsValid)
                return View(user);

            try
            {
                var response = await _authenticationService.Login(user);
            }
            catch (AuthException e)
            {
                var exception = e.Message;
                return View(user);
            }

            if (false)
                return View(user);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}

using EnterpriseApp.WebApp.MVC.Exceptions;
using EnterpriseApp.WebApp.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Polly.CircuitBreaker;
using Refit;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EnterpriseApp.WebApp.MVC.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private static IAuthenticationService _authenticationService;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;

            try
            {
                await _next(httpContext);
            }
            catch (CustomHttpRequestException e)
            {
                HandleRequestExceptionAsync(httpContext, e.StatusCode);
            }
            catch (ApiException e)
            {
                HandleRequestExceptionAsync(httpContext, e.StatusCode);
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuitException(httpContext);
            }
        }

        private static void HandleBrokenCircuitException(HttpContext context)
        {
            context.Response.Redirect($"/app-unavailable");
        }

        private static void HandleRequestExceptionAsync(HttpContext context, HttpStatusCode statusCode)
        {
            if (statusCode.Equals(HttpStatusCode.Unauthorized))
            {
                if (_authenticationService.IsExpiredToken() && _authenticationService.IsValidRefreshToken().Result)
                {
                    context.Response.Redirect(context.Request.Path);
                    return;
                }

                _authenticationService.Logout();

                context.Response.Redirect($"/login?ReturnUrl={context.Request.Path}");
                return;
            }

            context.Response.StatusCode = (int)statusCode;
        }
    }
}

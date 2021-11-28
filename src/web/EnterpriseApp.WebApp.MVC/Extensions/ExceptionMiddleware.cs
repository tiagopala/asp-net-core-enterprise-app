using EnterpriseApp.WebApp.MVC.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace EnterpriseApp.WebApp.MVC.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CustomHttpRequestException e)
            {
                HandleRequestExceptionAsync(httpContext, e);
            }
        }

        private static void HandleRequestExceptionAsync(HttpContext context, CustomHttpRequestException exception)
        {
            if (exception.StatusCode.Equals(HttpStatusCode.Unauthorized))
            {
                context.Response.Redirect("/login");
                return;
            }

            context.Response.StatusCode = (int)exception.StatusCode;
        }
    }
}

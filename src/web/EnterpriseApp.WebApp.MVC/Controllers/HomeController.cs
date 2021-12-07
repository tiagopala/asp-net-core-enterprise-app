using EnterpriseApp.WebApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseApp.WebApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        [Route("app-unavailable")]
        public IActionResult AppUnavailable()
        {
            var error = new ErrorViewModel
            {
                Title = "Oops! Apparently system is unavailable.",
                Message = "Oops! Apparently system is unavailable. This could be happening due increase number of users. Please try again in a few minutes.",
                ErrorCode = 500,
            };

            return View("Error", error);
        }

        [Route("Error/{statusCode:length(3,3)}")]
        public IActionResult Error(int statusCode)
        {
            var error = new ErrorViewModel();
            
            if (statusCode == 500)
            {
                error.Title = "An error occurred!";
                error.Message = "An Error occurred! Try again later.";
                error.ErrorCode = statusCode;
            }
            else if (statusCode == 404)
            {
                error.Title = "Oops! Page not found.";
                error.Message = "Oops! Page not found. The page you are looking for does not exist.";
                error.ErrorCode = statusCode;
            }
            else if (statusCode == 403)
            {
                error.Title = "Unauthorized";
                error.Message = "Unauthorized. You do not have permission to do this.";
                error.ErrorCode = statusCode;
            }
            else
            {
                return StatusCode(404);
            }

            return View("Error", error);
        }
    }
}

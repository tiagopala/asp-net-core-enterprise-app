using EnterpriseApp.Identidade.API.Extensions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace EnterpriseApp.API.Core.Controllers
{
    [ApiController]
    public abstract class MainController : Controller
    {
        protected ICollection<string> Errors = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            if (!CheckOperation())
                return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
                {
                    { "messages", Errors.ToArray() }
                }));

            return Ok(result);
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            modelState.GetModelStateErrors().ToList().ForEach(e => AddError(e));

            return CustomResponse();
        }

        protected ActionResult CustomResponse(IdentityResult result)
        {
            result.GetIdentityErrors().ToList().ForEach(e => AddError(e));

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            validationResult.Errors.ForEach(x => AddError(x.ErrorMessage));

            return CustomResponse();
        }

        protected bool CheckOperation()
            => !Errors.Any();

        protected void AddError(string error)
            => Errors.Add(error);

        protected void ClearErrors()
            => Errors.Clear();
    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace EnterpriseApp.Identidade.API.Extensions
{
    public static class ModelStateErrorExtension
    {
        public static IEnumerable<string> GetModelStateErrors(this ModelStateDictionary modelState)
            => modelState.Root.Children.SelectMany(y => y.Errors.Select(x => x.ErrorMessage));
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace EnterpriseApp.API.Core.Authentication
{
    // Método de autorização do usuário.
    // Esse método será chamado assim que passar pela annotation criada acima.
    // Sua função é validar o usuário por meio das Claims passadas em nossa annotation.
    public class RequiredClaimFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;

        public RequiredClaimFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
                context.Result = new UnauthorizedResult();

            if (!context.HttpContext.User.ValidateUserClaims(_claim.Type, _claim.Value))
                context.Result = new ForbidResult();
        }
    }
}

using System.Linq;
using System.Security.Claims;

namespace EnterpriseApp.API.Core.Authentication
{
    // Método responsável por válidar que o usuário possui as claims necessárias dentro do nosso JWT para acessar aquela determinada controller.
    public static class UserClaimsExtension
    {
        public static bool ValidateUserClaims(this ClaimsPrincipal user, string claimType, string claimValue)
            => user.Claims.Any(c => c.Type.Equals(claimType) && c.Value.Contains(claimValue));
    }
}

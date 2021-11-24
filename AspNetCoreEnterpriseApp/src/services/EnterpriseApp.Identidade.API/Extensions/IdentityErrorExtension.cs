using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace EnterpriseApp.Identidade.API.Extensions
{
    public static class IdentityErrorExtension
    {
        public static IEnumerable<string> GetIdentityErrors(this IdentityResult result)
            => result.Errors.Select(e => e.Description);

        public static string GetSignInError(this SignInResult result)
        {
            if (result.IsLockedOut)
                return "User login temporarily locked out.";
            else if (result.IsNotAllowed)
                return "User login not allowed.";
            else
                return "Incorrect authentication credentials.";
        }
    }
}

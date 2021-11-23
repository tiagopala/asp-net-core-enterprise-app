using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace EnterpriseApp.Identidade.API.Extensions
{
    public static class IdentityErrorExtension
    {
        public static IEnumerable<string> GetIdentityErrors(this IdentityResult result)
            => result.Errors.Select(e => e.Description);
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EnterpriseApp.API.Core.Authentication
{
    // Criação da minha annotation do tipo FilterAttribute. Exemplo: [ClaimsAuthorize("Fornecedor", "Adicionar")].
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimType, string claimValue) : base(typeof(RequiredClaimFilter))
        {
            Arguments = new object[] { new Claim(claimType, claimValue) };
        }
    }
}

using EnterpriseApp.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace EnterpriseApp.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Name => _httpContextAccessor.HttpContext.User.Identity.Name;

        public Guid GetUserId()
            => IsAuthenticated() ? Guid.Parse(GetClaimValue("sub")) : Guid.Empty;

        public string GetUserEmail()
            => IsAuthenticated() ? GetClaimValue("email") : string.Empty;

        public string GetUserToken()
            => IsAuthenticated() ? GetClaimValue("jwt") : string.Empty;

        public string GetRefreshToken()
            => IsAuthenticated() ? GetClaimValue("refreshToken") : string.Empty;

        public bool IsAuthenticated()
            => _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public IEnumerable<Claim> GetClaims()
            => _httpContextAccessor.HttpContext.User.Claims;

        public bool HasRole(string role)
            => _httpContextAccessor.HttpContext.User.IsInRole(role);

        public HttpContext GetHttpContext()
            => _httpContextAccessor.HttpContext;

        public string GetClaimValue(string claimType)
        {
            var claimValue = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == claimType)?.Value;

            if (claimValue is not null)
                return claimValue;

            if (claimType == "sub")
                return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))?.Value;

            if (claimType == "email")
                return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("email"))?.Value;

            return claimValue;
        }
    }
}

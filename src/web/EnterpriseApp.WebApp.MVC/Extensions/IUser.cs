using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace EnterpriseApp.WebApp.MVC.Extensions
{
    public interface IUser
    {
        string Name { get; }
        Guid GetUserId();
        string GetUserEmail();
        string GetUserToken();
        bool IsAuthenticated();
        bool HasRole(string role);
        IEnumerable<Claim> GetClaims();
        HttpContext GetHttpContext();
    }

    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AspNetUser(IHttpContextAccessor httpContextAccessor)
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

        public bool IsAuthenticated()
            => _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public string GetClaimValue(string claimType)
            => _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(claimType))?.Value;

        public IEnumerable<Claim> GetClaims()
            => _httpContextAccessor.HttpContext.User.Claims;

        public bool HasRole(string role)
            => _httpContextAccessor.HttpContext.User.IsInRole(role);

        public HttpContext GetHttpContext()
            => _httpContextAccessor.HttpContext;
    }
}

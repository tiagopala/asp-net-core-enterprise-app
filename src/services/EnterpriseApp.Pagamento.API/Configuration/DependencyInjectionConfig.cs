using EnterpriseApp.Core.Services;
using EnterpriseApp.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseApp.Cliente.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            #region Services
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserService, UserService>();
            #endregion

            return services;
        }
    }
}

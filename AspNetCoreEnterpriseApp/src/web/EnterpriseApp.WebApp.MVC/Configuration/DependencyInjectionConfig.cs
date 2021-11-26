using EnterpriseApp.WebApp.MVC.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseApp.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}

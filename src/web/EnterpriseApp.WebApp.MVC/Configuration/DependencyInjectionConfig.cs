using EnterpriseApp.WebApp.MVC.Extensions;
using EnterpriseApp.WebApp.MVC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseApp.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencyInjection(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthenticationService, AuthenticationService>();
            services.AddHttpClient<ICatalogService, CatalogService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUser, AspNetUser>();

            return services;
        }
    }
}

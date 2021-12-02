using EnterpriseApp.WebApp.MVC.Services;
using EnterpriseApp.WebApp.MVC.Services.Handlers;
using EnterpriseApp.WebApp.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EnterpriseApp.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            AuthAPIConfig authApiConfig = new();
            configuration.GetSection("AuthAPI").Bind(authApiConfig);

            AuthAPIConfig catalogApiConfig = new();
            configuration.GetSection("CatalogAPI").Bind(catalogApiConfig);

            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient("auth", configure =>
            {
                configure.BaseAddress = new Uri(authApiConfig.Endpoint);
            });

            services.AddHttpClient<ICatalogService, CatalogService>(configure => 
            {
                configure.BaseAddress = new Uri(catalogApiConfig.Endpoint);
            }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}

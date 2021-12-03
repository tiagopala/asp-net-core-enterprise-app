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
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAuthenticationService, AuthenticationService>(configure =>
            {
                configure.BaseAddress = new Uri(configuration.GetSection("AuthAPI").Get<AuthAPIConfig>().Endpoint);
            });

            //services.AddHttpClient<ICatalogService, CatalogService>(configure => 
            //{
            //    configure.BaseAddress = new Uri(configuration.GetSection("CatalogAPI").Get<CatalogApiConfig>().Endpoint);
            //}).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient("CatalogRefit", configure =>
            {
                configure.BaseAddress = new Uri(configuration.GetSection("CatalogAPI").Get<CatalogApiConfig>().Endpoint);
            }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
              .AddTypedClient(Refit.RestService.For<ICatalogServiceRefit>);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}

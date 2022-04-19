using EnterpriseApp.WebApp.MVC.Extensions;
using EnterpriseApp.WebApp.MVC.Services;
using EnterpriseApp.WebApp.MVC.Services.Handlers;
using EnterpriseApp.WebApp.MVC.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Refit;
using System;

namespace EnterpriseApp.WebApp.MVC.Configuration
{
    public static class HttpClientsConfig
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAuthenticationService, AuthenticationService>(configure =>
            {
                configure.BaseAddress = new Uri(configuration["AuthAPI:Uri"]);
            }).AddPolicyHandler(HttpCustomPolicyExtensions.GetHttpErrorWaitAndRetryCustomPolicy())
              .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            //services.AddHttpClient("CatalogRefit", configure =>
            //{
            //    configure.BaseAddress = new Uri(configuration["CatalogAPI:Uri"]);
            //}).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            //  .AddTypedClient(RestService.For<ICatalogServiceRefit>)
            //  .AddPolicyHandler(HttpCustomPolicyExtensions.GetHttpErrorWaitAndRetryCustomPolicy())
            //  .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<ICatalogService, CatalogService>(configure =>
            {
                configure.BaseAddress = new Uri(configuration["CatalogAPI:Uri"]);
            }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
              .AddPolicyHandler(HttpCustomPolicyExtensions.GetHttpErrorWaitAndRetryCustomPolicy())
              .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IPurchaseBffService, PurchaseBffService>(configure =>
            {
                configure.BaseAddress = new Uri($"{configuration["PurchaseBffServiceAPI:Uri"]}/api/purchase/");
            }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
              .AddPolicyHandler(HttpCustomPolicyExtensions.GetHttpErrorWaitAndRetryCustomPolicy())
              .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<ICustomersService, CustomersService>(configure =>
            {
                configure.BaseAddress = new Uri($"{configuration["CustomerAPI:Uri"]}/api/customers/");
            }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
              .AddPolicyHandler(HttpCustomPolicyExtensions.GetHttpErrorWaitAndRetryCustomPolicy())
              .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            return services;
        }
    }
}

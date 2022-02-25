﻿using EnterpriseApp.API.Core.Extensions;
using EnterpriseApp.BFF.Compras.AppSettings;
using EnterpriseApp.BFF.Compras.DelegatingHandlers;
using EnterpriseApp.BFF.Compras.Services;
using EnterpriseApp.BFF.Compras.Services.Interfaces;
using EnterpriseApp.Core.Services;
using EnterpriseApp.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;

namespace EnterpriseApp.BFF.Compras.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUserService, UserService>();

            services.AddHttpClientServices(configuration);

            return services;
        }

        public static IServiceCollection AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            var appServicesSettings = configuration.GetSection("ExternalServicesConfiguration").Get<ExternalServicesAppSettings>();

            services.AddHttpClient<ICatalogService, CatalogService>(x =>
            {
                x.BaseAddress = new Uri($"{appServicesSettings.CatalogUri}/api/catalog/");
            }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
              .AddPolicyHandler(PollyExtensions.GetHttpErrorWaitAndRetryCustomPolicy())
              .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IShoppingCartService, ShoppingCartService>(configuration =>
            {
                configuration.BaseAddress = new Uri($"{appServicesSettings.ShoppingCartUri}/api/");
            }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
              .AddPolicyHandler(PollyExtensions.GetHttpErrorWaitAndRetryCustomPolicy())
              .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IOrderService, OrderService>(configuration =>
            {
                configuration.BaseAddress = new Uri($"{appServicesSettings.OrderUri}/api/vouchers/");
            }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
              .AddPolicyHandler(PollyExtensions.GetHttpErrorWaitAndRetryCustomPolicy())
              .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IPaymentService, PaymentService>(configuration =>
            {
                configuration.BaseAddress = new Uri(appServicesSettings.PaymentUri);
            });

            return services;
        }
    }
}

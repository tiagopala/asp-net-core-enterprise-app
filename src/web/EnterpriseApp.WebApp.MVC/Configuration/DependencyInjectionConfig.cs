using EnterpriseApp.Core.Services;
using EnterpriseApp.Core.Services.Interfaces;
using EnterpriseApp.WebApp.MVC.Extensions;
using EnterpriseApp.WebApp.MVC.Services;
using EnterpriseApp.WebApp.MVC.Services.Handlers;
using EnterpriseApp.WebApp.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Refit;
using System;

namespace EnterpriseApp.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IValidationAttributeAdapterProvider, CpfValidationAttributeAdapterProvider>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #region Services
            services.AddScoped<IUserService, UserService>();
            #endregion

            return services;
        }
    }
}

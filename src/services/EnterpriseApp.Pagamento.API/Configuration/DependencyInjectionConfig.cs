using EnterpriseApp.Core.Services;
using EnterpriseApp.Core.Services.Interfaces;
using EnterpriseApp.Pagamento.API.AppSettings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseApp.Cliente.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PaymentConfig>(configuration.GetSection("PaymentConfig"));

            #region Services
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserService, UserService>();
            #endregion

            return services;
        }
    }
}

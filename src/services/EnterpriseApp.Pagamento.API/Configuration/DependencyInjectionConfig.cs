using EnterpriseApp.Core.Services;
using EnterpriseApp.Core.Services.Interfaces;
using EnterpriseApp.Pagamento.API.AppSettings;
using EnterpriseApp.Pagamento.API.Facade;
using EnterpriseApp.Pagamento.API.Repositories;
using EnterpriseApp.Pagamento.API.Repositories.Interfaces;
using EnterpriseApp.Pagamento.API.Services;
using EnterpriseApp.Pagamento.API.Services.Interfaces;
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

            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IPaymentFacade, PaymentCreditCardFacade>();
            #endregion

            #region Repositories
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            #endregion

            #region Background Services
            services.AddHostedService<PaymentIntegrationHandler>();
            #endregion

            return services;
        }
    }
}

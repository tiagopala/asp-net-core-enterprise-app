using EnterpriseApp.Cliente.API.Application.Commands;
using EnterpriseApp.Cliente.API.Application.Events;
using EnterpriseApp.Cliente.API.Application.Handlers;
using EnterpriseApp.Cliente.API.Business.Interfaces;
using EnterpriseApp.Cliente.API.Data.Repositories;
using EnterpriseApp.Cliente.API.Services;
using EnterpriseApp.Core.Mediator;
using EnterpriseApp.Core.Services;
using EnterpriseApp.Core.Services.Interfaces;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseApp.Cliente.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            #region Commands
            services
                .AddScoped<IMediatorHandler, MediatorHandler>()
                .AddScoped<IRequestHandler<RegisterCustomerCommand, ValidationResult>, CustomerHandler>()
                .AddScoped<IRequestHandler<AddAddressCommand, ValidationResult>, CustomerHandler>();
            #endregion;

            #region Events
            services
                .AddScoped<INotificationHandler<CustomerRegisteredEvent>, CustomerRegisteredEventHandler>();
            #endregion;

            #region Repositories
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            #endregion;

            #region Hoasted Services
            services.AddHostedService<RegisteredCustomerIntegrationHandler>();
            #endregion

            #region Services
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserService, UserService>();
            #endregion

            return services;
        }
    }
}

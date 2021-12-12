using EnterpriseApp.Cliente.API.Application.Commands;
using EnterpriseApp.Cliente.API.Application.Handlers;
using EnterpriseApp.Core.Mediator;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseApp.Cliente.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services
                .AddScoped<IMediatorHandler, MediatorHandler>()
                .AddScoped<IRequestHandler<RegisterCustomerCommand, ValidationResult>, RegisterCustomerHandler>();

            return services;
        }
    }
}

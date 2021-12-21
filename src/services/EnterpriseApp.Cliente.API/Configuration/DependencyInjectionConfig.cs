using EnterpriseApp.Cliente.API.Application.Commands;
using EnterpriseApp.Cliente.API.Application.Handlers;
using EnterpriseApp.Cliente.API.Business.Interfaces;
using EnterpriseApp.Cliente.API.Data.Repositories;
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
                .AddScoped<IRequestHandler<RegisterCustomerCommand, ValidationResult>, RegisterCustomerHandler>()
                .AddScoped<ICustomerRepository, CustomerRepository>();

            return services;
        }
    }
}

﻿using EnterpriseApp.Cliente.API.Application.Commands;
using EnterpriseApp.Cliente.API.Application.Events;
using EnterpriseApp.Cliente.API.Application.Handlers;
using EnterpriseApp.Cliente.API.Business.Interfaces;
using EnterpriseApp.Cliente.API.Data.Repositories;
using EnterpriseApp.Cliente.API.Services;
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
            #region Commands
            services
                .AddScoped<IMediatorHandler, MediatorHandler>()
                .AddScoped<IRequestHandler<RegisterCustomerCommand, ValidationResult>, RegisterCustomerHandler>();
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

            return services;
        }
    }
}

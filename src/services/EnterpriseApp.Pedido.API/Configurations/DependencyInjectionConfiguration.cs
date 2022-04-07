using EnterpriseApp.Core.Mediator;
using EnterpriseApp.Core.Services;
using EnterpriseApp.Core.Services.Interfaces;
using EnterpriseApp.Pedido.Application.BackgroundServices;
using EnterpriseApp.Pedido.Application.Commands;
using EnterpriseApp.Pedido.Application.Events;
using EnterpriseApp.Pedido.Application.Handlers;
using EnterpriseApp.Pedido.Application.Queries;
using EnterpriseApp.Pedido.Domain.Pedidos;
using EnterpriseApp.Pedido.Domain.Vouchers;
using EnterpriseApp.Pedido.Infrastructure.Repositories;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseApp.Pedido.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // API
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserService, UserService>();

            // MediatR
            services.AddMediatR(typeof(Startup));
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Commands
            services.AddScoped<IRequestHandler<AddOrderCommand, ValidationResult>, AddOrderCommandHandler>();

            // Events
            services.AddScoped<INotificationHandler<OrderRealizedEvent>, OrderEventHandler>();

            // Queries
            services.AddScoped<IOrderQueries, OrderQueries>();
            services.AddScoped<IVoucherQueries, VoucherQueries>();

            // Repositories
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IVoucherRepository, VoucherRepository>();

            // Hosted Services
            services.AddHostedService<OrderOrchestratorIntegrationHandler>();
            services.AddHostedService<OrderIntegrationHandler>();

            return services;
        }
    }
}

using EnterpriseApp.Core.Mediator;
using EnterpriseApp.Core.Services;
using EnterpriseApp.Core.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseApp.Pedido.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUserService, UserService>();

            services.AddMediatR(typeof(Startup));

            services.AddScoped<IMediatorHandler, MediatorHandler>();

            return services;
        }
    }
}

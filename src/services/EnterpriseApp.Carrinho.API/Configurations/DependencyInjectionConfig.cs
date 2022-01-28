using EnterpriseApp.Core.Services;
using EnterpriseApp.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseApp.Carrinho.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}

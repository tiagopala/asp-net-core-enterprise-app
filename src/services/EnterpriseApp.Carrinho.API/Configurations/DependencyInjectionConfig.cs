using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseApp.Carrinho.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services;
        }
    }
}

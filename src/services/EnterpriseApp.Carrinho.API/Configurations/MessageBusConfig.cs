using EnterpriseApp.Carrinho.API.BackgroundServices;
using EnterpriseApp.MessageBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseApp.Carrinho.API.Configurations
{
    public static class MessageBusConfig
    {
        public static IServiceCollection AddMessageBusConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration["MessageBusConnectionString"])
                .AddHostedService<CartIntegrationEventHandler>();

            return services;
        }
    }
}

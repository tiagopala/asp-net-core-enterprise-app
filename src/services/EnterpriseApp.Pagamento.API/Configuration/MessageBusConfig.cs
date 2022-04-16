using EnterpriseApp.MessageBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseApp.Cliente.API.Configuration
{
    public static class MessageBusConfig
    {
        public static IServiceCollection AddMessageBusConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration["MessageBusConnectionString"]);

            return services;
        }
    }
}

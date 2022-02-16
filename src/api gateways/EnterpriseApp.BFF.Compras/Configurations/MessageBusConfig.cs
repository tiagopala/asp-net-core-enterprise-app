using EnterpriseApp.BFF.Compras.AppSettings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseApp.BFF.Compras.Configurations
{
    public static class MessageBusConfig
    {
        public static IServiceCollection AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var messageBusConfig = configuration.GetSection("MessageBusConfiguration").Get<MessageBusAppSettings>();

            return services;
        }
    }
}

using EnterpriseApp.Catalogo.API.Data.Repositories;
using EnterpriseApp.Catalogo.API.Models;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseApp.Catalogo.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}

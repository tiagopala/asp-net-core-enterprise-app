using EnterpriseApp.API.Core.Authentication;
using EnterpriseApp.Identidade.API.Data;
using EnterpriseApp.Identidade.API.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Security.Jwt;
using NetDevPack.Security.Jwt.Store.EntityFrameworkCore;

namespace EnterpriseApp.Identidade.API.Configurations
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddJwksManager().PersistKeysToDatabaseStore<ApplicationDbContext>();

            // Configuração do ApplicationDbContext
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            // Configuração do Identity
            services
                .AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddErrorDescriber<IdentityPortugueseMessagesExtension>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}

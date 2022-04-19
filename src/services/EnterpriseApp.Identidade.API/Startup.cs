using EnterpriseApp.API.Core.Authentication;
using EnterpriseApp.API.Core.Documentation;
using EnterpriseApp.Identidade.API.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetDevPack.Security.Jwt.AspNetCore;

namespace EnterpriseApp.Identidade.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            if (hostEnvironment.IsDevelopment())
                builder.AddUserSecrets<Startup>();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Removendo a validação automática de model states para conseguirmos customizar o retorno
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services
                .AddRouting(options => options.LowercaseUrls = true)
                .AddIdentityConfig(Configuration)
                .AddJwtConfiguration(Configuration)
                .AddMessageBusConfig(Configuration)
                .AddServices()
                .AddSwaggerConfig("EnterpriseApp Identity API", "This API is responsible for taking care of user authentication e authorization services");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwaggerConfig()
               .UseHttpsRedirection()
               .UseRouting()
               .UseAuthConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseJwksDiscovery();
        }
    }
}

using EnterpriseApp.API.Core.Authentication;
using EnterpriseApp.API.Core.Documentation;
using EnterpriseApp.Cliente.API.Configuration;
using EnterpriseApp.Pagamento.API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EnterpriseApp.Pagamento.API
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
            services.AddControllers();

            services.AddDbContext<PaymentsContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddCors(options =>
            {
                options.AddPolicy("Total", builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services
                .AddRouting(x => x.LowercaseUrls = true)
                .AddJwtConfiguration(Configuration)
                .AddSwaggerConfig("EnterpriseApp Payment API", "This API is responsible for manage EnterpriseApp's payments")
                //.AddMediatR(typeof(Startup))
                .AddMessageBusConfig(Configuration)
                .AddAppServices(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwaggerConfig();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("Total");
            app.UseAuthConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

using EnterpriseApp.WebApp.MVC.Configuration;
using EnterpriseApp.WebApp.MVC.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EnterpriseApp.WebApp.MVC
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
            services.Configure<AuthAPIConfig>(Configuration.GetSection("AuthAPI"));
            services.Configure<CatalogApiConfig>(Configuration.GetSection("CatalogAPI"));
            services.ResolveIdentity();
            services.AddControllersWithViews();
            services.ResolveDependencyInjection();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            
            app.UseExceptionHandler("/Error/500");
            app.UseStatusCodePagesWithRedirects("/Error/{0}");
            app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.ResolveIdentity();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Catalog}/{action=Index}/{id?}");
            });
        }
    }
}

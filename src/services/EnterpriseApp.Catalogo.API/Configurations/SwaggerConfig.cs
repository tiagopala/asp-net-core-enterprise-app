﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace EnterpriseApp.Catalogo.API.Configurations
{
    public static class SwaggerConfig
    {
        public static IServiceCollection ResolveSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "EnterpriseApp Catalog API",
                    Description = "This API is responsible for manage EnterpriseApp's products.",
                    Contact = new OpenApiContact { Name = "Tiago Pala", Email = "tiago_pala@outlook.com" },
                    License = new OpenApiLicense { Name = "MIT", Url = new System.Uri("https://opensource.org/licenses/MIT") },
                    Version = "v1"
                });
            });

            return services;
        }

        public static IApplicationBuilder ResolveSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));

            return app;
        }
    }
}

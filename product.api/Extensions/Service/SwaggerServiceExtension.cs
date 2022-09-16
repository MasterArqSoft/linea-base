using Microsoft.OpenApi.Models;
using System.Reflection;

namespace product.api.Extensions.Service;

public static class SwaggerServiceExtension
{
    public static void AddSwaggerExtension(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Microservicio Producto - WebApi Net6 Core",
                Description = "Esta API será responsable de la distribución y autorización general de los datos de producto.",
                Contact = new OpenApiContact
                {
                    Name = "Gerencia de tecnología",
                    Email = "erwing.candelario@grupomok.com",
                    Url = new Uri("https://www.grupomok.com/"),
                },
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    },
                });
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });
    }
}

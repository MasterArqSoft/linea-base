using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace product.api.Extensions.Service;

public static class SecurityServiceExtension
{
    public static void AddAuthenticationExtension(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(opciones => opciones.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = false,
               ValidateAudience = false,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey(
                 Encoding.UTF8.GetBytes(Configuration["KeyJwt"])),
               ClockSkew = TimeSpan.Zero
           });
    }

    public static void AddCorsExtension(this IServiceCollection services)
    {
        services.AddCors(opciones =>
        {
            opciones.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
            });
        });
    }
}

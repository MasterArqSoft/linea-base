using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace product.api.Extensions.Service;

public static class SecurityServiceExtension
{
    public static void AddAuthenticationExtension(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(opciones => opciones.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidIssuer = Configuration["JWT:Issuer"],
               ValidAudience = Configuration["JWT:Audience"],
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey(
                 Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
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

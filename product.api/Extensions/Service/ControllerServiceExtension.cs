using FluentValidation.AspNetCore;
using microservice.core.Middlewares;

namespace product.api.Extensions.Service;

public static class ControllerServiceExtension
{
    public static void AddControllerExtension(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ValidateFilterAttribute>();
        }
        )
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
        })
        .AddFluentValidation(options =>
         {
             options.DisableDataAnnotationsValidation = true;
             options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic));
         })
        ;
    }
}

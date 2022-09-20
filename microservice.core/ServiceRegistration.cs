using microservice.core.Features.Products;
using microservice.core.Helpers;
using microservice.core.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace microservice.core;

public static class ServiceRegistration
{
    public static void AddCoreLayer(this IServiceCollection services)
    {
        services.AddTransient<IProductService, ProductServices>();
        services.AddTransient<ILogHelperService, LogHelperService>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}

using microservice.core.Interfaces.Repositories;
using microservice.dll.conection.Extensions;
using microservice.infrastructure.Repositories;
using microservice.infrastructure.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace microservice.infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MicroServiceContext>(options =>
        options.UseSqlServer(connectionString: Connection.GetPartnerConnection(2)));

        return services;
    }

    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        return services;
    }
}

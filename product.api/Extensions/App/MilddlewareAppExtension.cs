using microservice.domain.Middlewares;

namespace product.api.Extensions.App;

public static class MilddlewareAppExtension
{
    public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalErrorException>();
    }
}

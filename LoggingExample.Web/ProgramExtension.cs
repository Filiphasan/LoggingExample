using LoggingExample.Web.Middlewares;

namespace LoggingExample.Web;

public static class ProgramExtension
{
    public static IServiceCollection RegisterWeb(this IServiceCollection services)
    {
        services.RegisterServices();
        services.RegisterMiddlewares();

        return services;
    }

    public static IApplicationBuilder UseWeb(this IApplicationBuilder builder)
    {
        builder.UseMiddlewares();

        return builder;
    }

    private static void RegisterServices(this IServiceCollection services)
    {
        //Testing
    }

    private static void RegisterMiddlewares(this IServiceCollection services)
    {
        services.AddTransient<RequestResponseLogMiddleware>();
    }

    private static void UseMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<RequestResponseLogMiddleware>();
    }
}
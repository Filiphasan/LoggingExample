namespace LoggingExample.Web.Middlewares;

public class RequestResponseLogMiddleware(ILoggerFactory loggerFactory) : IMiddleware
{
    private readonly ILogger<RequestResponseLogMiddleware> _logger = loggerFactory.CreateLogger<RequestResponseLogMiddleware>();

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        _logger.LogInformation("Test Log");
        await next(context);
    }
}
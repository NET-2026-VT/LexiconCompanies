namespace Companies.API.Middleware;
// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class DemoMiddleware
{
    private readonly RequestDelegate _next;

    public DemoMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        Console.WriteLine("[Incomming request in DemoMiddleware]");
        await _next(httpContext);
        Console.WriteLine("[Ougoing response in DemoMiddleware]");
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class DemoMiddlewareExtensions
{
    public static IApplicationBuilder UseDemoMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<DemoMiddleware>();
    }
}

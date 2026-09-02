using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Companies.API.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(builder =>
        {
            builder.Run(async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    var problemDetails = new ProblemDetails
                    {
                        Status = context.Response.StatusCode,
                        Title = "Internal Server Error",
                        Detail = contextFeature.Error.Message,
                        Instance = context.Request.Path
                    };

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    //context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(problemDetails);
                }

            });
        });
    }
}

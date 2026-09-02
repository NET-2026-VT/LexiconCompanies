using Domain.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public abstract class ApiControllerBase : ControllerBase
{
    protected ActionResult ProcessError(ApiBaseResponse response)
    {
        return response switch
        {
            ApiNotFoundResponse notFoundResponse => NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = notFoundResponse.Message,
                Instance = Request.Path
            }),
            _ => throw new InvalidOperationException(
                $"The response type {response.GetType().Name} is not supported.")
        };
    }
}

using Companies.Shared.DTOs.CompanyDtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Companies.API.Filters;

public class ValidateRouteIdMatchAttribute : Attribute, IActionFilter
{

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var routeId = context.RouteData.Values["id"]?.ToString();
        var dto = context.ActionArguments.Values.OfType<UpdateCompanyDto>().FirstOrDefault();
        var dtoId = dto?.Id.ToString();

        if (routeId != dtoId)
            context.Result = new BadRequestObjectResult("Route id does not match dto id");

    }

    public void OnActionExecuted(ActionExecutedContext context) { }
   
}

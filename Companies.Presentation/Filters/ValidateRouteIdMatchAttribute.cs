using Companies.Shared.DTOs.CompanyDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Companies.Presentation.Filters;

[AttributeUsage(AttributeTargets.Method)]
public class ValidateRouteIdMatchAttribute : Attribute, IActionFilter
{
    private readonly Type typeName;

    public ValidateRouteIdMatchAttribute(Type typeName)
    {
        this.typeName = typeName;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var routeId = context.RouteData.Values["id"]?.ToString();
        //var dto = context.ActionArguments.Values.OfType<UpdateCompanyDto>().FirstOrDefault();
        //var dtoId = dto?.Id.ToString();
        var dto = context.ActionArguments.Values.FirstOrDefault(v => v?.GetType() == typeName);
        var dtoId = dto?.GetType().GetProperty("Id")?.GetValue(dto)?.ToString();

        if (routeId != dtoId)
            context.Result = new BadRequestObjectResult("Route id does not match dto id");

    }

    public void OnActionExecuted(ActionExecutedContext context) { }

}

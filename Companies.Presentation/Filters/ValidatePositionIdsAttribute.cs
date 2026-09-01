using Companies.Shared.DTOs;
using Companies.Shared.DTOs.CompanyDtos;
using Domain.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Companies.Presentation.Filters;

public class ValidatePositionIdsAttribute : IAsyncActionFilter
{
    private readonly IPositionRepsoitory _positionRepsoitory;

    public ValidatePositionIdsAttribute(IPositionRepsoitory positionRepsoitory)
    {
        _positionRepsoitory = positionRepsoitory;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var dto = context.ActionArguments.Values
                            .OfType<CreateCompanyDto>()
                            .FirstOrDefault();

        if (dto?.Employees is not null && dto.Employees.Any())
        {
            var positionIds = dto.Employees
                .Select(e => e.PositionId)
                .Distinct()
                .ToList();

            IEnumerable<Guid> validIds = await _positionRepsoitory.GetValidPositionIds(positionIds);

            var invalidIds = positionIds.Except(validIds).ToList();
           
            if (invalidIds.Any())
                context.Result = new NotFoundObjectResult($"Position(s) not found: {string.Join(", ", invalidIds)}");
            return;
        }


        await next();

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Exceptions;

public abstract class NotFoundException : Exception
{
    public string Title { get; }
    protected NotFoundException(string message, string title = "Not Found") : base(message)
    {
        Title = title;
    }
}

public sealed class CompanyNotFoundException(Guid id) : NotFoundException($"The company with id: {id} was not found")
{
}
public sealed class EmployeeNotFoundException(Guid id) : NotFoundException($"The employee with id: {id} was not found")
{
}

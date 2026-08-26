namespace Companies.Shared.DTOs.EmployeeDtos;

public record EmployeeDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public int Age { get; init; }
    public required string PositionName { get; init; }
}

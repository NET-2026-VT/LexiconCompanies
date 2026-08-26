using Companies.Shared.DTOs.EmployeeDtos;

namespace Companies.Shared.DTOs.CompanyDtos;

public record CompanyDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Address { get; init; }
    public IEnumerable<EmployeeDto> Employees { get; set; } = [];

}

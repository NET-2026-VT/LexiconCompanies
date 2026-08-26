using Companies.Shared.DTOs.EmployeeDtos;

namespace Companies.Shared.DTOs.CompanyDtos;

public record CreateCompanyDto : CompanyManipulationDto
{
    public IEnumerable<CreateEmployeeDto> Employees { get; set; } = [];
}

using Companies.API.Models.DTOs.EmployeeDtos;

namespace Companies.API.Models.DTOs.CompanyDtos;

public record CreateCompanyDto : CompanyManipulationDto
{
    public IEnumerable<CreateEmployeeDto> Employees { get; set; } = [];
}

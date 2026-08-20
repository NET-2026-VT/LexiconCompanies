namespace Companies.API.Models.DTOs.CompanyDtos;

public record UpdateCompanyDto : CompanyManipulationDto
{
    public Guid Id { get; set; }
}

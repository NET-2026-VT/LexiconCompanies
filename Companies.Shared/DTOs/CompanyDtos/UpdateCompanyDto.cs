namespace Companies.Shared.DTOs.CompanyDtos;

public record UpdateCompanyDto : CompanyManipulationDto
{
    public Guid Id { get; set; }
}

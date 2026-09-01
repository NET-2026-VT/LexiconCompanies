namespace Companies.Shared.DTOs.CompanyDtos;

public record UpdateCompanyDto : CompanyManipulationDto, IHasId
{
    public Guid Id { get; init; }
}

namespace Companies.API.Models.DTOs.CompanyDtos;

public record CompanyDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Address { get; init; }
    //public string? AddressCountry { get; set; }

}

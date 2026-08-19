namespace Companies.API.Models.DTOs.CompanyDtos;

public record CompanyDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public string? AddressCountry { get; set; }

}

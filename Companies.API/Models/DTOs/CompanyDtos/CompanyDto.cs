namespace Companies.API.Models.DTOs.CompanyDtos;

public record CompanyDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string AddressStreetAddress { get; set; }
    public required string AddressCity { get; set; }
    public string? Country { get; set; }

}

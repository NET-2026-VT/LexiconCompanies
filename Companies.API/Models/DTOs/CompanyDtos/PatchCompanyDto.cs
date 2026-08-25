using System.ComponentModel.DataAnnotations;

namespace Companies.API.Models.DTOs.CompanyDtos;

public record PatchCompanyDto
{
    [StringLength(60, MinimumLength = 2)]
    public string? Name { get; init; }

    [StringLength(60, MinimumLength = 5)]
    public string? StreetAddress { get; init; }

    [StringLength(60, MinimumLength = 2)]
    public string? City { get; init; }

    [StringLength(30, MinimumLength = 2)]
    public string? Country { get; init; }
}

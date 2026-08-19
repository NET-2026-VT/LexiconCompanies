using System.ComponentModel.DataAnnotations;

namespace Companies.API.Models.DTOs.CompanyDtos;

public record CreateCompanyDto
{
    [Display(Name = "Company name")]
    [Required(ErrorMessage = "{0} is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for {0} is {1} characters.")]
    [MinLength(2, ErrorMessage = "Minimum length for {0} is {1} characters.")]
    public required string Name { get; init; }

    [Display(Name = "Company street address")]
    [Required(ErrorMessage = "{0} is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for {0} is {1} characters.")]
    [MinLength(5, ErrorMessage = "Minimum length for {0} is {1} characters.")]
    public required string StreetAddress { get; init; }

    [Display(Name = "Company city")]
    [Required(ErrorMessage = "{0} is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for {0} is {1} characters.")]
    [MinLength(2, ErrorMessage = "Minimum length for {0} is {1} characters.")]
    public required string City { get; init; }

    [MaxLength(30, ErrorMessage = "Maximum length for {0} is {1} characters.")]
    [MinLength(2, ErrorMessage = "Minimum length for {0} is {1} characters.")]
    public string? Country { get; init; }
}

using System.ComponentModel.DataAnnotations;

namespace Companies.API.Models.DTOs.EmployeeDtos;

public record EmployeeManipulationDto
{
    [Required(ErrorMessage = "Employee name is a required field.")]
    [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
    [MinLength(2)]
    public string Name { get; init; } = null!;

    [Required(ErrorMessage = "Age is a required field.")]
    [Range(18, 90)]
    public int Age { get; init; }

    [Required]
    public Guid PositionId { get; init; }
}

public record CreateEmployeeDto : EmployeeManipulationDto { }
public record UpdateEmployeeDto : EmployeeManipulationDto { }

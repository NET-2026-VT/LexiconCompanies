using Companies.Shared.DTOs.EmployeeDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Companies.Shared.DTOs.AuthDtos;

public record UserRegistrationDto : EmployeeManipulationDto
{
    [Required]
    [EmailAddress]
    public string Email { get; init; } = null!;

    [Required]
    public string UserName { get; init; } = null!;

    [Required]
    public string Password { get; init; } = null!;

    [Required]
    public string Role { get; init; } = null!;
}


using Companies.Shared.DTOs.AuthDtos;
using Microsoft.AspNetCore.Identity;

namespace Service.Contracts;

public interface IAuthService
{
    Task<IdentityResult> RegisterUserAsync(UserRegistrationDto userRegistrationDto);
}
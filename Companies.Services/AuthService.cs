using Companies.Shared.DTOs.AuthDtos;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Companies.Services;

public class AuthService : IAuthService
{
    public Task<IdentityResult> RegisterUserAsync(UserRegistrationDto userRegistrationDto)
    {
        throw new NotImplementedException();
    }
}

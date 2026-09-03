using Companies.Shared.DTOs.AuthDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Companies.Presentation.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IServiceManager serviceManager) : ControllerBase
{
    private readonly IServiceManager _serviceManager = serviceManager;

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser(UserRegistrationDto userRegistrationDto)
    {
        IdentityResult identityResult = await _serviceManager.AuthService.RegisterUserAsync(userRegistrationDto);

        return identityResult.Succeeded
            ? StatusCode(StatusCodes.Status201Created)
            : BadRequest(identityResult.Errors);

    }
}

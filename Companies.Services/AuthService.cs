using AutoMapper;
using Companies.Shared.DTOs.AuthDtos;
using Domain.Contracts;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Companies.Services;

public class AuthService : IAuthService
{
    private readonly IMapper mapper;
    private readonly UserManager<Employee> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly IUnitOfWork unitOfWork;

    public AuthService(
        IMapper mapper,
        UserManager<Employee> userManager,
        RoleManager<IdentityRole> roleManager,
        IUnitOfWork unitOfWork
        )
    {
        this.mapper = mapper;
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.unitOfWork = unitOfWork;
    }
    public async Task<IdentityResult> RegisterUserAsync(UserRegistrationDto userRegistrationDto)
    {
        var roleExists = await roleManager.RoleExistsAsync(userRegistrationDto.Role);

        if (!roleExists)
            return IdentityResult.Failed(new IdentityError { Description = "Role does not exist" });

        //ToDo: Create AnyAsync in repo!
        var companyExists = await unitOfWork.CompanyRepsoitory.GetCompany(userRegistrationDto.CompanyId);

        if (companyExists is null)
            return IdentityResult.Failed(new IdentityError { Description = "Company does not exist" });

        var positionExists = await unitOfWork.PositionRepsoitory.AnyAsync(userRegistrationDto.PositionId);

        if (!positionExists)
            return IdentityResult.Failed(new IdentityError { Description = "Position does not exist" });

        var user = mapper.Map<Employee>(userRegistrationDto);

        var result = await userManager.CreateAsync(user, userRegistrationDto.Password);

        if (result.Succeeded)
            await userManager.AddToRoleAsync(user, userRegistrationDto.Role);

        return result;
    }
}

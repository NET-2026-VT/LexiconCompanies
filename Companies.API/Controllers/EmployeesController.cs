using AutoMapper;
using Companies.API.Data;
using Companies.API.Models.DTOs.EmployeeDtos;
using Companies.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Companies.API.Controllers;


[Route("api/companies/{companyId}/employees")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public EmployeesController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees(Guid companyId)
    {
        var companyExists = await _context.Companies.AnyAsync(c => c.Id.Equals(companyId));
        if (!companyExists) return NotFound();

        List<EmployeeDto> employeeDtos = await _mapper.ProjectTo<EmployeeDto>
                     (_context.Emplpoyees.Where(e => e.CompanyId.Equals(companyId)))
                     .ToListAsync();

        return employeeDtos;
                    
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> PostEmployee(Guid companyId, CreateEmployeeDto dto)
    {

    }

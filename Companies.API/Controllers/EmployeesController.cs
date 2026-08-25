using AutoMapper;
using AutoMapper.QueryableExtensions;
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
                     (_context.Employees.Where(e => e.CompanyId.Equals(companyId)))
                     .ToListAsync();

        return employeeDtos;

    }

    [HttpGet("{id}", Name = "GetEmployeeById")]
    public async Task<ActionResult<EmployeeDto>> GetEmployee(Guid companyId, Guid id)
    {
        var companyExists = await _context.Companies.AnyAsync(c => c.Id.Equals(companyId));
        if (!companyExists) return NotFound();

        var dto = await _mapper.ProjectTo<EmployeeDto>(_context.Employees
                               .Where(e => e.Id == id && e.CompanyId == companyId))
                               .FirstOrDefaultAsync();

        if (dto is null) return NotFound();

        return dto;
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> PostEmployee(Guid companyId, CreateEmployeeDto dto)
    {
        var companyExists = await _context.Companies.AnyAsync(c => c.Id.Equals(companyId));
        if (!companyExists) return NotFound($"Company with id {companyId} not found.");

        var existsinPosition = await _context.Positions.FirstOrDefaultAsync(p => p.Id.Equals(dto.PositionId));
        if (existsinPosition is null) return NotFound($"Position with id {dto.PositionId} not found.");

        var employee = _mapper.Map<Employee>(dto);
        employee.CompanyId = companyId;

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        //employee.Position = existsinPosition;

        var created = _mapper.Map<EmployeeDto>(employee);

        return CreatedAtRoute("GetEmployeeById", new { companyId, id = created.Id }, created);
    }
}

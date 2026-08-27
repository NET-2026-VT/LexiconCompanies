//using AutoMapper;
//using Companies.Shared.DTOs.EmployeeDtos;
//using Microsoft.AspNetCore.JsonPatch.SystemTextJson;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace Companies.Presentation.Controllers;


//[Route("api/companies/{companyId}/employees")]
//[ApiController]
//public class EmployeesController : ControllerBase
//{
//    private readonly ApplicationDbContext _context;
//    private readonly IMapper _mapper;

//    public EmployeesController(ApplicationDbContext context, IMapper mapper)
//    {
//        _context = context;
//        _mapper = mapper;
//    }

//    [HttpGet]
//    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees(Guid companyId)
//    {
//        var companyExists = await _context.Companies.AnyAsync(c => c.Id.Equals(companyId));
//        if (!companyExists) return NotFound();

//        List<EmployeeDto> employeeDtos = await _mapper.ProjectTo<EmployeeDto>
//                     (_context.Employees.Where(e => e.CompanyId.Equals(companyId)))
//                     .ToListAsync();

//        return employeeDtos;

//    }

//    [HttpGet("{id}", Name = "GetEmployeeById")]
//    public async Task<ActionResult<EmployeeDto>> GetEmployee(Guid companyId, Guid id)
//    {
//        var companyExists = await _context.Companies.AnyAsync(c => c.Id.Equals(companyId));
//        if (!companyExists) return NotFound();

//        var dto = await _mapper.ProjectTo<EmployeeDto>(_context.Employees
//                               .Where(e => e.Id == id && e.CompanyId == companyId))
//                               .FirstOrDefaultAsync();

//        if (dto is null) return NotFound();

//        return dto;
//    }

//    [HttpPost]
//    public async Task<ActionResult<EmployeeDto>> PostEmployee(Guid companyId, CreateEmployeeDto dto)
//    {
//        var companyExists = await _context.Companies.AnyAsync(c => c.Id.Equals(companyId));
//        if (!companyExists) return NotFound($"Company with id {companyId} not found.");

//        var existsinPosition = await _context.Positions.FirstOrDefaultAsync(p => p.Id.Equals(dto.PositionId));
//        if (existsinPosition is null) return NotFound($"Position with id {dto.PositionId} not found.");

//        var employee = _mapper.Map<Employee>(dto);
//        employee.CompanyId = companyId;

//        _context.Employees.Add(employee);
//        await _context.SaveChangesAsync();

//        //employee.Position = existsinPosition;

//        var created = _mapper.Map<EmployeeDto>(employee);

//        return CreatedAtRoute("GetEmployeeById", new { companyId, id = created.Id }, created);
//    }


//    //Requires content-type header to "application/json-patch+json"
//    [HttpPatch("{id}")]
//    public async Task<IActionResult> PatchEmployee(Guid companyId, Guid id, JsonPatchDocument<UpdateEmployeeDto> patchDocument)
//    {
//        var companyExists = await _context.Companies.AnyAsync(c => c.Id.Equals(companyId));

//        if (!companyExists) return Problem(
//            statusCode: StatusCodes.Status404NotFound,
//            title: "Company not found",
//            detail: $"Company with id:{companyId} could not be located",
//            instance: Request.Path.ToString()
//            );

//        var employeeToPatch = await _context.Employees
//                                           .Include(e => e.Position)
//                                           .FirstOrDefaultAsync(e => e.Id == id && e.CompanyId == companyId);

//        if (employeeToPatch is null)
//            return Problem(
//                title: "Employee not found.",
//                detail: $"No employee with id {id} exists for company {companyId}.",
//                statusCode: StatusCodes.Status404NotFound,
//                instance: Request.Path.ToString());

//        var employeeToPatchDto = _mapper.Map<UpdateEmployeeDto>(employeeToPatch);

//        patchDocument.ApplyTo(employeeToPatchDto,
//            e => ModelState.AddModelError(e.Operation.path ?? "JsonPatch", e.ErrorMessage)); // Not valid patch-operations
//        if (!ModelState.IsValid) return BadRequest(ModelState);

//        TryValidateModel(employeeToPatchDto);                  // Validate Dto (Attributes)
//        if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

//        _mapper.Map(employeeToPatchDto, employeeToPatch);
//        await _context.SaveChangesAsync();


//        return Ok(_mapper.Map<EmployeeDto>(employeeToPatch)); //just for demo
//                                                              // return NoContent();
//    }

//    [HttpDelete("{id}")]
//    public async Task<IActionResult> DeleteEmployee(Guid companyId, Guid id)
//    {
//        var company = await _context.Companies.AnyAsync(c => c.Id.Equals(companyId));
//        if (!company)
//            return Problem(
//                title: "Company not found.",
//                detail: $"No company with id {companyId} exists.",
//                statusCode: StatusCodes.Status404NotFound,
//                instance: Request.Path.ToString());

//        var employee = await _context.Employees
//                                     .FirstOrDefaultAsync(e => e.Id == id && e.CompanyId == companyId);
//        if (employee is null)
//            return Problem(
//                title: "Employee not found.",
//                detail: $"No employee with id {id} exists for company {companyId}.",
//                statusCode: StatusCodes.Status404NotFound,
//                instance: Request.Path.ToString());

//        _context.Employees.Remove(employee);
//        await _context.SaveChangesAsync();

//        return NoContent();
//    }
//}

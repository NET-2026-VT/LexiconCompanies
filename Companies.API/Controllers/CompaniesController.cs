using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Companies.API.Models.Entities;
using Companies.API.Data;
using Companies.API.Models.DTOs.CompanyDtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;

[Route("api/companies")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CompaniesController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompany()
    {
        var dto = await _mapper.ProjectTo<CompanyDto>(_context.Companies).ToListAsync();
        return dto;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CompanyDto>> GetCompany(Guid id)
    {
        var dto = await _context.Companies.Where(c => c.Id == id)
                                          .ProjectTo<CompanyDto>(_mapper.ConfigurationProvider)
                                          .FirstOrDefaultAsync();

        //var dto2 = await _mapper.ProjectTo<CompanyDto>(_context.Companies.Where(c => c.Id == id))
        //                        .FirstOrDefaultAsync();

        if (dto == null) return NotFound();

        return dto;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCompany(Guid? id, UpdateCompanyDto dto)
    {
        if (id != dto.Id) return BadRequest();

        var existingCompany =  await _context.Companies
                                            .Include(c => c.Address)
                                            .FirstOrDefaultAsync(c => c.Id.Equals(id));   
        
        if(existingCompany == null) return NotFound();

        _mapper.Map(dto, existingCompany);

        await _context.SaveChangesAsync();

        return Ok(_mapper.Map<CompanyDto>(existingCompany)); //For Demo
        //return NoContent();
    }


    [HttpPost]
    public async Task<ActionResult<Company>> PostCompany(CreateCompanyDto dto)
    {
        var company = _mapper.Map<Company>(dto);

        _context.Companies.Add(company);
        await _context.SaveChangesAsync();

        var created = _mapper.Map<CompanyDto>(company);

        return CreatedAtAction(nameof(GetCompany), new { id = created.Id }, created);
    }

    //// DELETE: api/Company/5
    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteCompany(System.Guid? id)
    //{
    //    var company = await _context.Companies.FindAsync(id);
    //    if (company == null)
    //    {
    //        return NotFound();
    //    }

    //    _context.Companies.Remove(company);
    //    await _context.SaveChangesAsync();

    //    return NoContent();
    //}

    //private bool CompanyExists(System.Guid? id)
    //{
    //    return _context.Companies.Any(e => e.Id == id);
    //}
}

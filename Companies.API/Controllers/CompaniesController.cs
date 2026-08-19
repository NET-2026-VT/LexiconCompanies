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

    // GET: api/Company
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompany()
    {
        //In memory
        //var companies = await _context.Companies.Include(c => c.Address).ToListAsync();
        //var debugDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

        ////Projection 1
        //var debugDto2 = await _context.Companies.ProjectTo<CompanyDto>(_mapper.ConfigurationProvider).ToListAsync();

        //Projection 2
        var dto = await _mapper.ProjectTo<CompanyDto>(_context.Companies).ToListAsync();

        //With select
        //var dto = await _context.Companies.Select(c => new CompanyDto
        //{
        //    Id = c.Id,
        //    Name = c.Name,
        //    StreetAddress = c.Address.StreetAddress,
        //    City = c.Address.City,
        //    Country = c.Address.Country

        //}).ToListAsync();

        return dto;
    }

    // GET: api/Company/5
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

    //// PUT: api/Company/5
    //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //[HttpPut("{id}")]
    //public async Task<IActionResult> PutCompany(System.Guid? id, Company company)
    //{
    //    if (id != company.Id)
    //    {
    //        return BadRequest();
    //    }

    //    _context.Entry(company).State = EntityState.Modified;

    //    try
    //    {
    //        await _context.SaveChangesAsync();
    //    }
    //    catch (DbUpdateConcurrencyException)
    //    {
    //        if (!CompanyExists(id))
    //        {
    //            return NotFound();
    //        }
    //        else
    //        {
    //            throw;
    //        }
    //    }

    //    return NoContent();
    //}

    //// POST: api/Company
    //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //[HttpPost]
    //public async Task<ActionResult<Company>> PostCompany(Company company)
    //{
    //    _context.Companies.Add(company);
    //    await _context.SaveChangesAsync();

    //    return CreatedAtAction("GetCompany", new { id = company.Id }, company);
    //}

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

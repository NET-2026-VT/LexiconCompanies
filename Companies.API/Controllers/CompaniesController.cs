using AutoMapper;
using AutoMapper.QueryableExtensions;
using Companies.API.Models.DTOs.CompanyDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompany(bool includeEmployees)
    {
        //var dto = includeEmployees ? await _mapper.ProjectTo<CompanyDto>(
        //                                                _context.Companies.Include(c => c.Employees))
        //                                               .ToListAsync() :

        //                             await _mapper.ProjectTo<CompanyDto>(
        //                                                _context.Companies)
        //                                                .ToListAsync();

        var query = _context.Companies.Include(c => c.Address);

        var dto2 = includeEmployees ? _mapper.Map<IEnumerable<CompanyDto>>(await query.Include(c => c.Employees)
                                                            .ThenInclude(e => e.Position)
                                                            .ToListAsync()) :

                                     _mapper.Map<IEnumerable<CompanyDto>>(await query.ToListAsync());
        return Ok(dto2);
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

        var existingCompany = await _context.Companies
                                            .Include(c => c.Address)
                                            .FirstOrDefaultAsync(c => c.Id.Equals(id));

        if (existingCompany == null) return NotFound();

        _mapper.Map(dto, existingCompany);

        await _context.SaveChangesAsync();

        return Ok(_mapper.Map<CompanyDto>(existingCompany)); //For Demo
        //return NoContent();
    }


    [HttpPost]
    public async Task<ActionResult<Company>> PostCompany(CreateCompanyDto dto)
    {
        if (dto.Employees is not null && dto.Employees.Any())
        {
            var positionIds = dto.Employees.Select(e => e.PositionId).Distinct().ToList();
            var validIds = await _context.Positions
                                         .Where(p => positionIds.Contains(p.Id))
                                         .Select(p => p.Id)
                                         .ToListAsync();

            var invalidIds = positionIds.Except(validIds).ToList();
            if (invalidIds.Any())
                return NotFound($"Position(s) not found: {string.Join(", ", invalidIds)}");
        }


        var company = _mapper.Map<Company>(dto);

        _context.Companies.Add(company);
        await _context.SaveChangesAsync();

        // var created = _mapper.Map<CompanyDto>(company);
        var created = await _context.Companies
                                    .Where(c => c.Id == company.Id)
                                    .ProjectTo<CompanyDto>(_mapper.ConfigurationProvider)
                                    .FirstAsync();

        return CreatedAtAction(nameof(GetCompany), new { id = created.Id }, created);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompany(Guid? id)
    {
        var company = await _context.Companies
                                .FirstOrDefaultAsync(c => c.Id.Equals(id));

        if (company == null) return NotFound();

        _context.Companies.Remove(company);
        await _context.SaveChangesAsync();

        return NoContent();
    }


    [HttpPatch("{id}")]
    public async Task<ActionResult<CompanyDto>> PatchCompany(Guid id, PatchCompanyDto dto)
    {
        var company = await _context.Companies
                                    .Include(c => c.Address)
                                    .FirstOrDefaultAsync(c => c.Id == id);

        if (company is null) return NotFound();

        // Uppdatera bara de fält som faktiskt skickades med (inte null)
        if (dto.Name is not null) company.Name = dto.Name;
        if (dto.StreetAddress is not null) company.Address.StreetAddress = dto.StreetAddress;
        if (dto.City is not null) company.Address.City = dto.City;
        if (dto.Country is not null) company.Address.Country = dto.Country;

        await _context.SaveChangesAsync();

        return Ok(_mapper.Map<CompanyDto>(company));
    }

}

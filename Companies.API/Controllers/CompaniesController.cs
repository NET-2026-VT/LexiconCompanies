using AutoMapper;
using AutoMapper.QueryableExtensions;
using Companies.Infrastructure.Repositories;
using Companies.Shared.DTOs.CompanyDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/companies")]
[ApiController]
public class CompaniesController : ControllerBase
{
   // private readonly ApplicationDbContext _context;
    private readonly ICompanyRepsoitory companyRepsoitory;
    private readonly IPositionRepsoitory positionRepsoitory;
    private readonly IMapper _mapper;

    public CompaniesController(ApplicationDbContext context, ICompanyRepsoitory companyRepsoitory,IPositionRepsoitory positionRepsoitory, IMapper mapper)
    {
      //  _context = context;
        this.companyRepsoitory = companyRepsoitory;
        this.positionRepsoitory = positionRepsoitory;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetAllCompany(bool includeEmployees)
    {
        var dto2 = _mapper.Map<IEnumerable<CompanyDto>>(await companyRepsoitory.GetCompanies(includeEmployees));

        return Ok(dto2);
    }

  

    [HttpGet("{id}")]
    public async Task<ActionResult<CompanyDto>> GetCompanyById(Guid id, bool includeEmployees)
    {
        var dto = _mapper.Map<CompanyDto>(await companyRepsoitory.GetCompany(id, includeEmployees));

        if (dto == null) return NotFound();

        return dto;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCompany(Guid id, UpdateCompanyDto dto)
    {
        if (id != dto.Id) return BadRequest();

        var existingCompany = await companyRepsoitory.GetCompany(id);

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
            IEnumerable<Guid> validIds = await positionRepsoitory.GetValidPositionIds(positionIds);

            var invalidIds = positionIds.Except(validIds).ToList();
            if (invalidIds.Any())
                return NotFound($"Position(s) not found: {string.Join(", ", invalidIds)}");
        }


        var company = _mapper.Map<Company>(dto);
        companyRepsoitory.Create(company);
        await _context.SaveChangesAsync();

        //ToDo fix position
        var created = _mapper.Map<CompanyDto>(await companyRepsoitory.GetCompany(company.Id));

        return CreatedAtAction(nameof(GetCompanyById), new { id = created.Id }, created);
    }

   

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompany(Guid id)
    {
        var company = await companyRepsoitory.GetCompany(id);

        if (company == null) return NotFound();

        companyRepsoitory.Delete(company);
        await _context.SaveChangesAsync();

        return NoContent();
    }


    [HttpPatch("{id}")]
    public async Task<ActionResult<CompanyDto>> PatchCompany(Guid id, PatchCompanyDto dto)
    {
        var company = await companyRepsoitory.GetCompany(id);

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

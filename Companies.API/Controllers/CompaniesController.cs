using AutoMapper;
using Companies.Shared.DTOs.CompanyDtos;
using Microsoft.AspNetCore.Mvc;

[Route("api/companies")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public CompaniesController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetAllCompany(bool includeEmployees)
    {
        var dto2 = _mapper.Map<IEnumerable<CompanyDto>>(await _uow.CompanyRepsoitory.GetCompanies(includeEmployees));

        return Ok(dto2);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CompanyDto>> GetCompanyById(Guid id, bool includeEmployees)
    {
        var dto = _mapper.Map<CompanyDto>(await _uow.CompanyRepsoitory.GetCompany(id, includeEmployees));

        if (dto == null) return NotFound();

        return dto;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCompany(Guid id, UpdateCompanyDto dto)
    {
        if (id != dto.Id) return BadRequest();

        var existingCompany = await _uow.CompanyRepsoitory.GetCompany(id);

        if (existingCompany == null) return NotFound();

        _mapper.Map(dto, existingCompany);

        await _uow.CompleteAsync();

        return Ok(_mapper.Map<CompanyDto>(existingCompany)); //For Demo
        //return NoContent();
    }


    [HttpPost]
    public async Task<ActionResult<Company>> PostCompany(CreateCompanyDto dto)
    {
        if (dto.Employees is not null && dto.Employees.Any())
        {
            var positionIds = dto.Employees.Select(e => e.PositionId).Distinct().ToList();
            IEnumerable<Guid> validIds = await _uow.PositionRepsoitory.GetValidPositionIds(positionIds);

            var invalidIds = positionIds.Except(validIds).ToList();
            if (invalidIds.Any())
                return NotFound($"Position(s) not found: {string.Join(", ", invalidIds)}");
        }


        var company = _mapper.Map<Company>(dto);
        _uow.CompanyRepsoitory.Create(company);
        await _uow.CompleteAsync();

        //ToDo fix position
        var created = _mapper.Map<CompanyDto>(await _uow.CompanyRepsoitory.GetCompany(company.Id));

        return CreatedAtAction(nameof(GetCompanyById), new { id = created.Id }, created);
    }



    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompany(Guid id)
    {
        var company = await _uow.CompanyRepsoitory.GetCompany(id);

        if (company == null) return NotFound();

        _uow.CompanyRepsoitory.Delete(company);
        await _uow.CompleteAsync();

        return NoContent();
    }


    [HttpPatch("{id}")]
    public async Task<ActionResult<CompanyDto>> PatchCompany(Guid id, PatchCompanyDto dto)
    {
        var company = await _uow.CompanyRepsoitory.GetCompany(id);

        if (company is null) return NotFound();

        // Uppdatera bara de fält som faktiskt skickades med (inte null)
        if (dto.Name is not null) company.Name = dto.Name;
        if (dto.StreetAddress is not null) company.Address.StreetAddress = dto.StreetAddress;
        if (dto.City is not null) company.Address.City = dto.City;
        if (dto.Country is not null) company.Address.Country = dto.Country;

        await _uow.CompleteAsync();

        return Ok(_mapper.Map<CompanyDto>(company));
    }

}

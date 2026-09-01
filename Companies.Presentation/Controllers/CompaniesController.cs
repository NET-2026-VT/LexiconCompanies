using Companies.Presentation.Filters;
using Companies.Shared.DTOs.CompanyDtos;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

[Route("api/companies")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public CompaniesController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetAllCompany(bool includeEmployees)
    {
        var dto = await _serviceManager.CompanyService.GetCompaniesAsync(includeEmployees);

        return Ok(dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CompanyDto>> GetCompanyById(Guid id, bool includeEmployees)
    {
        var dto = await _serviceManager.CompanyService.GetCompanyAsync(id, includeEmployees);

        return dto;
    }

    [HttpPut("{id}")]
    [ValidateRouteIdMatch(typeof(UpdateCompanyDto))]
    public async Task<ActionResult<CompanyDto>> PutCompany(Guid id, UpdateCompanyDto dto)
    {
        // if (id != dto.Id) return BadRequest(); 

        var updatedCompany = await _serviceManager.CompanyService.UpdateCompanyAsync(id, dto);
        return updatedCompany; //For Demo
        //return NoContent();
    }


    //[HttpPost]
    //public async Task<ActionResult<Company>> PostCompany(CreateCompanyDto dto)
    //{
    //    if (dto.Employees is not null && dto.Employees.Any())
    //    {
    //        var positionIds = dto.Employees.Select(e => e.PositionId).Distinct().ToList();
    //        IEnumerable<Guid> validIds = await _uow.PositionRepsoitory.GetValidPositionIds(positionIds);

    //        var invalidIds = positionIds.Except(validIds).ToList();
    //        if (invalidIds.Any())
    //            return NotFound($"Position(s) not found: {string.Join(", ", invalidIds)}");
    //    }


    //    var company = _mapper.Map<Company>(dto);
    //    _uow.CompanyRepsoitory.Create(company);
    //    await _uow.CompleteAsync();

    //    //ToDo fix position
    //    var created = _mapper.Map<CompanyDto>(await _uow.CompanyRepsoitory.GetCompany(company.Id));

    //    return CreatedAtAction(nameof(GetCompanyById), new { id = created.Id }, created);
    //}



    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteCompany(Guid id)
    //{
    //    var company = await _uow.CompanyRepsoitory.GetCompany(id);

    //    if (company == null) return NotFound();
    //    _uow.CompanyRepsoitory.Delete(company);
    //    await _uow.CompleteAsync();

    //    return NoContent();
    //}


    //[HttpPatch("{id}")]
    //public async Task<ActionResult<CompanyDto>> PatchCompany(Guid id, PatchCompanyDto dto)
    //{
    //    var company = await _uow.CompanyRepsoitory.GetCompany(id, trackChanges: true);

    //    if (company is null) return NotFound();

    //    // Uppdatera bara de fält som faktiskt skickades med (inte null)
    //    if (dto.Name is not null) company.Name = dto.Name;
    //    if (dto.StreetAddress is not null) company.Address.StreetAddress = dto.StreetAddress;
    //    if (dto.City is not null) company.Address.City = dto.City;
    //    if (dto.Country is not null) company.Address.Country = dto.Country;

    //    await _uow.CompleteAsync();

    //    return Ok(_mapper.Map<CompanyDto>(company));
    //}

}

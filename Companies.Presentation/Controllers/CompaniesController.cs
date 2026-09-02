using Companies.Presentation.Extensions;
using Companies.Presentation.Filters;
using Companies.Shared.DTOs.CompanyDtos;
using Companies.Shared.Paging;
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
    public async Task<ActionResult<PagedResponse<CompanyDto>>> GetAllCompany([FromQuery]CompanyQueryParameters query)
    {
        var response = await _serviceManager.CompanyService.GetCompaniesAsync(query);

        var companies = response.GetResult<PagedResponse<CompanyDto>>();

        return Ok(companies);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CompanyDto>> GetCompanyById(Guid id, bool includeEmployees)
    {
        var response = await _serviceManager.CompanyService.GetCompanyAsync(id, includeEmployees);

        return response.GetResult<CompanyDto>();
    }

    [HttpPut("{id}")]
    [ValidateRouteIdMatch]
    public async Task<ActionResult<CompanyDto>> PutCompany(Guid id, UpdateCompanyDto dto)
    {
        // if (id != dto.Id) return BadRequest(); 

        var response = await _serviceManager.CompanyService.UpdateCompanyAsync(id, dto);



        return response.GetResult<CompanyDto>(); //For Demo
        //return NoContent();
    }


    [HttpPost]
    [TypeFilter(typeof(ValidatePositionIdsAttribute))]
    public async Task<ActionResult<CompanyDto>> PostCompany(CreateCompanyDto dto)
    {
        var response = await _serviceManager.CompanyService.CreateCompanyAsync(dto);

        
        var created = response.GetResult<CompanyDto>();

        return CreatedAtAction(nameof(GetCompanyById), new { id = created.Id }, created);
    }



    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompany(Guid id)
    {
        var response = await _serviceManager.CompanyService.DeleteCompanyAsync(id);

        return NoContent();
    }


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

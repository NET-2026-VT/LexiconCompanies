using AutoMapper;
using Companies.Shared.DTOs.CompanyDtos;
using Companies.Shared.Paging;
using Domain.Contracts;
using Domain.Models.Entities;
using Service.Contracts;

namespace Companies.Services;

public class CompanyService : ICompanyService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public CompanyService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<PagedResponse<CompanyDto>> GetCompaniesAsync(CompanyQueryParameters query, bool trackChanges = false)
    {
        IPagedList<Company> pagedList = await _uow.CompanyRepsoitory.GetCompanies(query, trackChanges);
        var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(pagedList.Items);

        return new(companyDtos, query.PageNumber, query.PageSize, pagedList.TotalCount);

    }

    public async Task<CompanyDto> GetCompanyAsync(Guid id, bool includeEmployees, bool trackChanges = false)
    {
        var dto = _mapper.Map<CompanyDto>(await _uow.CompanyRepsoitory.GetCompany(id, includeEmployees, trackChanges));

        if (dto == null) throw new Exception("Company not found");

        return dto;
    }

    public async Task<CompanyDto> UpdateCompanyAsync(Guid id, UpdateCompanyDto dto)
    {
        var existingCompany = await _uow.CompanyRepsoitory.GetCompany(id, trackChanges: true);

        if (existingCompany == null) return null!; //ToDo: Fix! //return NotFound();

        _mapper.Map(dto, existingCompany);

        await _uow.CompleteAsync();

        return _mapper.Map<CompanyDto>(existingCompany); //For Demo
    }

    public async Task<CompanyDto> CreateCompanyAsync(CreateCompanyDto dto)
    {
        //Validate
        var company = _mapper.Map<Company>(dto);
        _uow.CompanyRepsoitory.Create(company);
        await _uow.CompleteAsync();

        var created = dto.Employees.Any() ?
            _mapper.Map<CompanyDto>(await _uow.CompanyRepsoitory.GetCompany(company.Id, includeEmployees: true)) :
            _mapper.Map<CompanyDto>(await _uow.CompanyRepsoitory.GetCompany(company.Id));

        return created;
       
    }

    public async Task DeleteCompanyAsync(Guid id)
    {
        var company = await _uow.CompanyRepsoitory.GetCompany(id);

        if (company == null) Console.WriteLine("");
        _uow.CompanyRepsoitory.Delete(company);
        await _uow.CompleteAsync();
    }
}

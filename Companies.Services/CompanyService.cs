using AutoMapper;
using Companies.Shared.DTOs.CompanyDtos;
using Companies.Shared.Paging;
using Domain.Contracts;
using Domain.Models.Entities;
using Domain.Models.Responses;
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

    public async Task<ApiBaseResponse> GetCompaniesAsync(CompanyQueryParameters query, bool trackChanges = false)
    {
        IPagedList<Company> pagedList = await _uow.CompanyRepsoitory.GetCompanies(query, trackChanges);
        var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(pagedList.Items);

        PagedResponse<CompanyDto> pagedResponse = new(companyDtos, query.PageNumber, query.PageSize, pagedList.TotalCount);
        return new ApiOkResponse<PagedResponse<CompanyDto>>(pagedResponse);
    }

    public async Task<ApiBaseResponse> GetCompanyAsync(Guid id, bool includeEmployees, bool trackChanges = false)
    {
        var dto = _mapper.Map<CompanyDto>(await _uow.CompanyRepsoitory.GetCompany(id, includeEmployees, trackChanges));

        if (dto == null) return new CompanyNotFoundResponse(id);

        return new ApiOkResponse<CompanyDto>(dto);
    }

    public async Task<ApiBaseResponse> UpdateCompanyAsync(Guid id, UpdateCompanyDto dto)
    {
        var existingCompany = await _uow.CompanyRepsoitory.GetCompany(id, trackChanges: true);

        if (existingCompany == null) return new CompanyNotFoundResponse(id);

        _mapper.Map(dto, existingCompany);

        await _uow.CompleteAsync();

        var updatedCompany = _mapper.Map<CompanyDto>(existingCompany); 
        return new ApiOkResponse<CompanyDto>(updatedCompany); //For Demo
    }

    public async Task<ApiBaseResponse> CreateCompanyAsync(CreateCompanyDto dto)
    {
        //Validate
        var company = _mapper.Map<Company>(dto);
        _uow.CompanyRepsoitory.Create(company);
        await _uow.CompleteAsync();

        var created = dto.Employees.Any() ?
            _mapper.Map<CompanyDto>(await _uow.CompanyRepsoitory.GetCompany(company.Id, includeEmployees: true)) :
            _mapper.Map<CompanyDto>(await _uow.CompanyRepsoitory.GetCompany(company.Id));

        return new ApiOkResponse<CompanyDto>(created);

    }

    public async Task<ApiBaseResponse> DeleteCompanyAsync(Guid id)
    {
        var company = await _uow.CompanyRepsoitory.GetCompany(id);

        if (company == null) return new CompanyNotFoundResponse(id);
        _uow.CompanyRepsoitory.Delete(company);
        await _uow.CompleteAsync();

        return new ApiNoContentResponse();
    }
}

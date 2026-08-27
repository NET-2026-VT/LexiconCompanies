using AutoMapper;
using Companies.Shared.DTOs.CompanyDtos;
using Domain.Contracts;
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

    public async Task<IEnumerable<CompanyDto>> GetCompaniesAsync(bool includeEmployees, bool trackChanges = false)
    {
        var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(await _uow.CompanyRepsoitory.GetCompanies(includeEmployees, trackChanges));

        return companyDtos;
    }

    public async Task<CompanyDto> GetCompanyAsync(Guid id, bool includeEmployees, bool trackChanges = false)
    {
        var dto = _mapper.Map<CompanyDto>(await _uow.CompanyRepsoitory.GetCompany(id, includeEmployees, trackChanges));

        if (dto == null) return null!; //Todo handle response  //NotFound();

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
}
    
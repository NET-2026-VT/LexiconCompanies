using Companies.Shared.DTOs.CompanyDtos;
using Companies.Shared.Paging;

namespace Service.Contracts;

public interface ICompanyService
{
    Task<IEnumerable<CompanyDto>> GetCompaniesAsync(CompanyQueryParameters query, bool trackChanges = false);
    Task<CompanyDto> GetCompanyAsync(Guid id, bool includeEmployees, bool trackChanges = false);
    Task<CompanyDto> UpdateCompanyAsync(Guid id, UpdateCompanyDto dto);
    Task<CompanyDto> CreateCompanyAsync(CreateCompanyDto dto);
}

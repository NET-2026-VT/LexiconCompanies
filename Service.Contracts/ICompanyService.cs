using Companies.Shared.DTOs.CompanyDtos;

namespace Service.Contracts;

public interface ICompanyService
{
    Task<IEnumerable<CompanyDto>> GetCompaniesAsync(bool includeEmployees, bool trackChanges = false);
    Task<CompanyDto> GetCompanyAsync(Guid id, bool includeEmployees, bool trackChanges = false);
}

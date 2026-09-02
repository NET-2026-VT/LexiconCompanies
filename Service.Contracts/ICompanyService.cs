using Companies.Shared.DTOs.CompanyDtos;
using Companies.Shared.Paging;
using Domain.Models.Responses;

namespace Service.Contracts;

public interface ICompanyService
{
    Task<ApiBaseResponse> GetCompaniesAsync(CompanyQueryParameters query, bool trackChanges = false);
    Task<ApiBaseResponse> GetCompanyAsync(Guid id, bool includeEmployees, bool trackChanges = false);
    Task<ApiBaseResponse> UpdateCompanyAsync(Guid id, UpdateCompanyDto dto);
    Task<ApiBaseResponse> CreateCompanyAsync(CreateCompanyDto dto);
    Task<ApiBaseResponse> DeleteCompanyAsync(Guid id);
}

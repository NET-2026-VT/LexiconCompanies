using Domain.Models.Entities;
using Companies.Shared;
using Companies.Shared.Paging;

namespace Domain.Contracts;

public interface ICompanyRepsoitory : IRepositoryBase<Company>
{
    Task<IPagedList<Company>> GetCompanies(CompanyQueryParameters query, bool trackChanges = false);
    Task<Company?> GetCompany(Guid id, bool includeEmployees = false, bool trackChanges = false);
}
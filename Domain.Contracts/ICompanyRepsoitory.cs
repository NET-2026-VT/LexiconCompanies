using Domain.Models.Entities;

namespace Domain.Contracts;

public interface ICompanyRepsoitory : IRepositoryBase<Company>
{
    Task<IEnumerable<Company>> GetCompanies(bool includeEmployees = false, bool trackChanges = false);
    Task<Company?> GetCompany(Guid id, bool includeEmployees = false, bool trackChanges = false);
}
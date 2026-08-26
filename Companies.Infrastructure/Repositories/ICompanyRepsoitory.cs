using Domain.Models.Entities;

namespace Companies.Infrastructure.Repositories;

public interface ICompanyRepsoitory
{
    Task<IEnumerable<Company>> GetCompanies(bool includeEmployees = false);
    Task<Company?> GetCompany(Guid id, bool includeEmployees = false);
}
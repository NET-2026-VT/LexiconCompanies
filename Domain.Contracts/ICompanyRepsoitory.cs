using Domain.Models.Entities;

namespace Domain.Contracts;

public interface ICompanyRepsoitory
{
    Task<IEnumerable<Company>> GetCompanies(bool includeEmployees = false);
    Task<Company?> GetCompany(Guid id, bool includeEmployees = false);
    void Create(Company company);
    void Delete(Company company);
}
using Companies.Infrastructure.Data;
using Companies.Shared.Paging;
using Domain.Contracts;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Companies.Infrastructure.Repositories;

public class CompanyRepsoitory : RepositoryBase<Company>, ICompanyRepsoitory
{
    private readonly ApplicationDbContext _context;

    public CompanyRepsoitory(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Company>> GetCompanies(CompanyQueryParameters query, bool trackChanges = false)
    {
        var company = GetCompanyQuery(query.IncludeEmployees, trackChanges);

        //add pagination to query.


        return await company.ToListAsync();
    }

    public async Task<Company?> GetCompany(Guid id, bool includeEmployees = false, bool trackChanges = false)
    {
        var company = GetCompanyQuery(includeEmployees, trackChanges);
        return await company.FirstOrDefaultAsync(c => c.Id == id);
    }



    private IQueryable<Company> GetCompanyQuery(bool includeEmployees, bool trackChanges)
    {
        return includeEmployees ? FindAll(trackChanges)
                                    .Include(c => c.Address)
                                    .Include(c => c.Employees)
                                    .ThenInclude(e => e.Position) :

                                   FindAll(trackChanges)
                                    .Include(c => c.Address);
    }
}

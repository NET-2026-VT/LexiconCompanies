using Companies.Infrastructure.Data;
using Domain.Contracts;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Companies.Infrastructure.Repositories;

public class CompanyRepsoitory : ICompanyRepsoitory
{
    private readonly ApplicationDbContext _context;

    public CompanyRepsoitory(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Company>> GetCompanies(bool includeEmployees = false)
    {
        var company = GetCompanyQuery(includeEmployees);
        return await company.ToListAsync();
    }

    public async Task<Company?> GetCompany(Guid id, bool includeEmployees = false)
    {
        var company = GetCompanyQuery(includeEmployees);
        return await company.FirstOrDefaultAsync(c => c.Id == id);
    }

    public void Create(Company company) => _context.Companies.Add(company);
    public void Delete(Company company) => _context.Companies.Remove(company);

    private IQueryable<Company> GetCompanyQuery(bool includeEmployees)
    {
        return includeEmployees ? _context.Companies
                                    .Include(c => c.Address)
                                    .Include(c => c.Employees)
                                    .ThenInclude(e => e.Position) :

                                  _context.Companies
                                    .Include(c => c.Address);
    }
}

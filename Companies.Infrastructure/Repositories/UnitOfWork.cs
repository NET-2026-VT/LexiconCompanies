using Companies.Infrastructure.Data;
using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Companies.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public ICompanyRepsoitory CompanyRepsoitory { get; }
    public IPositionRepsoitory PositionRepsoitory { get; }

    public UnitOfWork(ApplicationDbContext context, ICompanyRepsoitory companyRepsoitory, IPositionRepsoitory positionRepsoitory)
    {
        _context = context;
        this.CompanyRepsoitory = companyRepsoitory;
        PositionRepsoitory = positionRepsoitory;
    }

    public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
}

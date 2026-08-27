using Companies.Infrastructure.Data;
using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Companies.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private Lazy<ICompanyRepsoitory> _companyRepsoitory;
    private Lazy<IPositionRepsoitory> _positionRepsoitory;
    public ICompanyRepsoitory CompanyRepsoitory => _companyRepsoitory.Value;
    public IPositionRepsoitory PositionRepsoitory => _positionRepsoitory.Value;

    public UnitOfWork(ApplicationDbContext context, Lazy<ICompanyRepsoitory> companyRepsoitory, Lazy<IPositionRepsoitory> positionRepsoitory)
    {
        _context = context;
        _companyRepsoitory = companyRepsoitory;
        _positionRepsoitory = positionRepsoitory;
    }

    public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
}

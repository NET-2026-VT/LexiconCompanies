using Domain.Contracts;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Companies.Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<ICompanyRepsoitory> _companyRepsoitory;
    public ICompanyRepsoitory CompanyRepsoitory => _companyRepsoitory.Value;

    public IUnitOfWork UoW { get; }

    public ServiceManager(IUnitOfWork uow, Lazy<ICompanyRepsoitory> companyRepsoitory)
    {
        UoW = uow;
        _companyRepsoitory = companyRepsoitory;
    }
}

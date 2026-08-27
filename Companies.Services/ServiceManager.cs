using Domain.Contracts;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Companies.Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<ICompanyService> _companyService;
    public ICompanyService CompanyService => _companyService.Value;

    public ServiceManager(Lazy<ICompanyService> companyService)
    {
        _companyService = companyService;
    }
}

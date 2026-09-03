using Service.Contracts;

namespace Companies.Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<ICompanyService> _companyService;
    private readonly Lazy<IAuthService> _authService;

    public ICompanyService CompanyService => _companyService.Value;
    public IAuthService AuthService => _authService.Value;

    public ServiceManager(Lazy<ICompanyService> companyService, Lazy<IAuthService> authService)
    {
        _companyService = companyService;
        _authService = authService;
    }
}

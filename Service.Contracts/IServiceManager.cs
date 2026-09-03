namespace Service.Contracts;

public interface IServiceManager
{
    ICompanyService CompanyService { get; }
    IAuthService AuthService { get; }
}
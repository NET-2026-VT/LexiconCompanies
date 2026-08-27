using Domain.Contracts;

namespace Service.Contracts;

public interface IServiceManager
{
    ICompanyRepsoitory CompanyRepsoitory { get; }
    IUnitOfWork UoW { get; }
}
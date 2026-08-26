namespace Domain.Contracts;

public interface IUnitOfWork
{
    ICompanyRepsoitory CompanyRepsoitory { get; }
    IPositionRepsoitory PositionRepsoitory { get; }

    Task<int> CompleteAsync();
}
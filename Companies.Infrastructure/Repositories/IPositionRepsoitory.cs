namespace Companies.Infrastructure.Repositories;

public interface IPositionRepsoitory
{
    Task<IEnumerable<Guid>> GetValidPositionIds(List<Guid> positionIds);
}
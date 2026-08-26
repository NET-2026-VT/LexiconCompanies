namespace Domain.Contracts;

public interface IPositionRepsoitory
{
    Task<IEnumerable<Guid>> GetValidPositionIds(List<Guid> positionIds);
}
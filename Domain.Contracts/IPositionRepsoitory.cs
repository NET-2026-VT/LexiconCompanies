using Companies.Shared.DTOs.AuthDtos;

namespace Domain.Contracts;

public interface IPositionRepsoitory
{
    Task<IEnumerable<Guid>> GetValidPositionIds(List<Guid> positionIds);
    Task<bool> AnyAsync(Guid positionId);
}
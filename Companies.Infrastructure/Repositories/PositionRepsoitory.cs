using Companies.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Companies.Infrastructure.Repositories;

public class PositionRepsoitory
{
    private readonly ApplicationDbContext _context;

    public PositionRepsoitory(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Guid>> GetValidPositionIds(List<Guid> positionIds)
    {
        return await _context.Positions
                                     .Where(p => positionIds.Contains(p.Id))
                                     .Select(p => p.Id)
                                     .ToListAsync();
    }
}

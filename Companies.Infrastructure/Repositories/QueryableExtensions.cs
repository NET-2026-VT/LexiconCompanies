using Microsoft.EntityFrameworkCore;

namespace Companies.Infrastructure.Repositories;

public static class QueryableExtensions
{
    public static IQueryable<T> WithTracking<T>(this IQueryable<T> query, bool trackChanges) where T : class
    {
        return trackChanges ? query : query.AsNoTracking();
    }
}
 
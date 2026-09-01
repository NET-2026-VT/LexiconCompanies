using Companies.Infrastructure.Paging;
using Companies.Shared.Paging;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Companies.Infrastructure.Extensions;

public static class QueryableExtensions
{
    public static async Task<IPagedList<T>> ToPagedListAsync<T>(
        this IQueryable<T> query,
        QueryParameters parameters) where T : class
    {
        var totalCount = await query.CountAsync();
        var items = await query
                         .Skip(parameters.GetOffset)
                         .Take(parameters.PageSize)
                         .ToListAsync();

        return new PagedList<T>(items,  totalCount);
    }

    public static IQueryable<T> WithTracking<T>(this IQueryable<T> query, bool trackChanges) where T : class
    {
        return trackChanges ? query : query.AsNoTracking();
    }
}

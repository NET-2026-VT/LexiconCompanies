using Companies.Infrastructure.Data;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Companies.Infrastructure.Repositories;

public abstract class RepositoryBase<T> where T : class
{
    private readonly ApplicationDbContext _context;
    protected DbSet<T> DbSet { get; }

    public RepositoryBase(ApplicationDbContext context)
    {
        _context = context;
        DbSet = context.Set<T>();
    }

    public IQueryable<T> FindAll(bool trackChanges = false) => 
                      DbSet.WithTracking(trackChanges);

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false) =>
                     DbSet.WithTracking(trackChanges).Where(expression); 

    public void Create(T entity) => _context.Add(entity);
    public void Delete(T entity) => _context.Remove(entity);
}

public static class QueryableExtensions
{
    public static IQueryable<T> WithTracking<T>(this IQueryable<T> query, bool trackChanges) where T : class
    {
        return trackChanges ? query : query.AsNoTracking();
    }
}
 
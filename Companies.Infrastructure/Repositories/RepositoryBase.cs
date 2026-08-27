using Companies.Infrastructure.Data;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Companies.Infrastructure.Repositories;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    private readonly ApplicationDbContext _context;
    protected DbSet<T> DbSet { get; }

    public RepositoryBase(ApplicationDbContext context)
    {
        _context = context;
        DbSet = context.Set<T>();
    }

    protected IQueryable<T> FindAll(bool trackChanges = false) =>
                      DbSet.WithTracking(trackChanges);

    protected IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false) =>
                     DbSet.WithTracking(trackChanges).Where(expression);

    public void Create(T entity) => _context.Add(entity);
    public void Delete(T entity) => _context.Remove(entity);
}

using System.Linq.Expressions;

namespace Domain.Contracts;

public interface IRepositoryBase<T> where T : class
{
    void Create(T entity);
    void Delete(T entity);
    //IQueryable<T> FindAll(bool trackChanges = false);
    //IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);
}


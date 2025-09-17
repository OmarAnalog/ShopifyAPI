using Shopify.Domain.Entities;
using System.Linq.Expressions;

namespace Shopify.Domain.Repositories
{
    public interface IBaseRepository<T> where T:class
    {
        IQueryable<T> GetAllAsync(bool TrackChanges);
        IQueryable<T> FindbyCondition(Expression<Func<T, bool>> expression, bool TrackChanges);
        Task AddAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
    }
}

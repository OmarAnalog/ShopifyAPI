using Microsoft.EntityFrameworkCore;
using Shopify.Domain.Repositories;
using System.Linq.Expressions;

namespace Shopify.Infrastructure.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ShopifyDbContext _dbContext;

        public BaseRepository(ShopifyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(T entity)
        => await _dbContext.Set<T>().AddAsync(entity);

        public void DeleteAsync(T entity)
        => _dbContext.Set<T>().Remove(entity);

        public IQueryable<T> FindbyCondition(Expression<Func<T, bool>> expression, bool TrackChanges)
        =>TrackChanges ? _dbContext.Set<T>().Where(expression) :
            _dbContext.Set<T>().Where(expression).AsNoTracking();

        public IQueryable<T> GetAllAsync(bool TrackChanges)
        => TrackChanges? _dbContext.Set<T>() : 
            _dbContext.Set<T>().AsNoTracking();

        public void UpdateAsync(T entity)
        => _dbContext.Set<T>().Update(entity);
    }
}

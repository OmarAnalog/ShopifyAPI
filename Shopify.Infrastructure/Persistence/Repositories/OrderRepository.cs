using Microsoft.EntityFrameworkCore;
using Shopify.Domain.Entities;
using Shopify.Domain.Repositories;

namespace Shopify.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly ShopifyDbContext _dbContext;

        public OrderRepository(ShopifyDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateOrderAsync(Order order)
        {
            await _dbContext.AddAsync(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id, bool trackChanges)
        => await FindbyCondition(o => o.Id == id, trackChanges).Include(o=>o.OrderItems).FirstOrDefaultAsync();
    }
}

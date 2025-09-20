using Shopify.Domain.Entities;

namespace Shopify.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task CreateOrderAsync(Order order);
        Task<Order?> GetOrderByIdAsync(int id,bool trackChanges);
    }
}

using Shopify.Domain.Entities;

namespace Shopify.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(bool trackChanges);
        Task<Product> GetProductByIdAsync(int productId, bool trackChanges);
    }
}

using Microsoft.EntityFrameworkCore;
using Shopify.Domain.Entities;
using Shopify.Domain.Repositories;
using System.Linq.Expressions;

namespace Shopify.Infrastructure.Persistence.Repositories
{
    public class ProductRepository :BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ShopifyDbContext context):base(context)
        {
            
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(bool trackChanges)
        => await GetAllAsync(trackChanges).ToListAsync();

        public async Task<Product?> GetProductByIdAsync(int productId, bool trackChanges)
        => await FindbyCondition(p => p.Id == productId, trackChanges).FirstOrDefaultAsync();
    }
}

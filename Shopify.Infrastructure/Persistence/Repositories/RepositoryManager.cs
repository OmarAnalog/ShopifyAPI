using Shopify.Domain.Repositories;

namespace Shopify.Infrastructure.Persistence.Repositories
{
    internal class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IProductRepository> productRepository;
        private readonly ShopifyDbContext _dbContext;
        public RepositoryManager(ShopifyDbContext dbContext)
        {
            _dbContext = dbContext;
            productRepository = new Lazy<IProductRepository>(() => new ProductRepository(_dbContext));
        }
        public IProductRepository ProductRepository => productRepository.Value;
        public async Task SaveAsync()
        => await _dbContext.SaveChangesAsync();
    }
}

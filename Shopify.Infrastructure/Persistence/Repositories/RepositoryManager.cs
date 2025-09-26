using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using Shopify.Application.Services;
using Shopify.Domain.Entities.Identity;
using Shopify.Domain.Repositories;

namespace Shopify.Infrastructure.Persistence.Repositories
{
    internal class RepositoryManager : IRepositoryManager, IDisposable
    {
        private readonly Lazy<IProductRepository> productRepository;
        private readonly Lazy<IOrderRepository> orderRepository;
        private readonly ShopifyDbContext _dbContext;
        private IDbContextTransaction? _dbContextTransaction;
        private readonly Lazy<IUserRepository> userRepository;
        public RepositoryManager(ShopifyDbContext dbContext,UserManager<User> userManager,ITokenService tokenService)
        {
            _dbContext = dbContext;
            productRepository = new Lazy<IProductRepository>(() => new ProductRepository(_dbContext));
            orderRepository = new Lazy<IOrderRepository>(() => new OrderRepository(_dbContext));
            userRepository = new Lazy<IUserRepository>(() => new AuthenticationRepository(userManager,tokenService));
        }
        public IProductRepository ProductRepository => productRepository.Value;
        public IOrderRepository OrderRepository => orderRepository.Value;

        public IUserRepository UserRepository => userRepository.Value;

        public async Task BeginTransaction()
        {
            if (_dbContextTransaction != null)
            {
                return;
            }
            _dbContextTransaction=await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaction()
        {
            try
            {
                if (_dbContextTransaction is null) return;
                await _dbContext.SaveChangesAsync();
                await _dbContextTransaction.CommitAsync();
            }
            finally
            {
                if (_dbContextTransaction is not null)
                {
                    await _dbContextTransaction.DisposeAsync();
                    _dbContextTransaction = null;
                }
            }
        }

        public void Dispose()
        {
            _dbContextTransaction?.Dispose();
        }

        public async Task RollbackTransaction()
        {
            if (_dbContextTransaction is not null)
            {
                await _dbContextTransaction.RollbackAsync();
                await _dbContextTransaction.DisposeAsync();
                _dbContextTransaction = null;
            }
        }

        public async Task SaveAsync()
        => await _dbContext.SaveChangesAsync();
    }
}

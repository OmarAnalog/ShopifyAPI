namespace Shopify.Domain.Repositories
{
    public interface IRepositoryManager
    {
        IProductRepository ProductRepository { get; }
        IOrderRepository OrderRepository { get; }
        IAuthenticationRepository AuthenticationRepository { get; }
        Task BeginTransaction();
        Task CommitTransaction();
        Task RollbackTransaction();
        Task SaveAsync();
    }
}

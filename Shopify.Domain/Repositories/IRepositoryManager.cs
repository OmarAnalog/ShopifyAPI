namespace Shopify.Domain.Repositories
{
    public interface IRepositoryManager
    {
        IProductRepository ProductRepository { get; }
        IOrderRepository OrderRepository { get; }
        IUserRepository UserRepository { get; }
        Task BeginTransaction();
        Task CommitTransaction();
        Task RollbackTransaction();
        Task SaveAsync();
    }
}

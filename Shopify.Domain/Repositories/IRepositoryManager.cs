namespace Shopify.Domain.Repositories
{
    public interface IRepositoryManager
    {
        IProductRepository ProductRepository { get; }
        Task SaveAsync();
    }
}

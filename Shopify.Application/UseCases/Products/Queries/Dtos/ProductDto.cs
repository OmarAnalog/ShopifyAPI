using Shopify.Domain.Entities;

namespace Shopify.Application.UseCases.Products.Queries.Dtos
{
    public record ProductDto
    (int Id, string Name, string? Description, decimal Price, int StockQuantity)
    {
        public static ProductDto FactoryProduct(Product product) =>
            new ProductDto
            (
                product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.Stock
            );
    }
}

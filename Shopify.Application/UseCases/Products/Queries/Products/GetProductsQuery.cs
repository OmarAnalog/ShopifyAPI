using MediatR;
using Shopify.Application.UseCases.Products.Queries.Dtos;

namespace Shopify.Application.UseCases.Products.Queries.Products
{
    public class GetProductsQuery : IRequest<IEnumerable<ProductDto>>
    {
    }
}

using MediatR;
using Shopify.Application.Products.Queries.Dtos;

namespace Shopify.Application.Products.Queries.Products
{
    public class GetProductsQuery:IRequest<IEnumerable<ProductDto>>
    {
    }
}

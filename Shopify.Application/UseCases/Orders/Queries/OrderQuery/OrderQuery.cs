using MediatR;
using Shopify.Domain.Entities;

namespace Shopify.Application.UseCases.Orders.Queries.OrderQuery
{
    public class OrderQuery : IRequest<Order>
    {
        public int Id { get; set; }
        public OrderQuery(int id) => Id = id;
    }
}

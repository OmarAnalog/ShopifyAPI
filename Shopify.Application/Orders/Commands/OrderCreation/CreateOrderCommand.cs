using MediatR;
using Shopify.Application.Orders.Dtos;

namespace Shopify.Application.Orders.Commands.OrderCreation
{
    public class CreateOrderCommand : IRequest<int>
    {
        public CreateOrderDto OrderDto { get; set; }
        public CreateOrderCommand(CreateOrderDto orderDto)
        => OrderDto = orderDto;
    }
}

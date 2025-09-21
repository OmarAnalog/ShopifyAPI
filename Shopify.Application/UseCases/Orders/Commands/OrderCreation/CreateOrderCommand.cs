using MediatR;
using Shopify.Application.UseCases.Orders.Dtos;

namespace Shopify.Application.UseCases.Orders.Commands.OrderCreation
{
    public class CreateOrderCommand : IRequest<int>
    {
        public CreateOrderDto OrderDto { get; set; }
        public CreateOrderCommand(CreateOrderDto orderDto)
        => OrderDto = orderDto;
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopify.Application.UseCases.Orders.Commands.OrderCreation;
using Shopify.Application.UseCases.Orders.Queries.OrderQuery;
using Shopify.Application.UseCases.Orders.Dtos;

namespace Shopify.Presentation.Controllers
{
    [ApiController]
    [Route("Orders")]
    public class OrdersController:ControllerBase
    {
        private readonly ISender _mediatr;
        public OrdersController(ISender mediatr)
        {
            _mediatr = mediatr;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderDto order)
        {
            var validator = new CreateOrderValidator();
            var validationResult = validator.Validate(order);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var command = new CreateOrderCommand(order);
            var orderId = await _mediatr.Send(command);
            return CreatedAtAction(nameof(GetOrderByIdAsync), new {orderId});
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderByIdAsync(int id)
        {
            var order = await _mediatr.Send(new OrderQuery(id));
            return Ok(order);
        }
    }
}

using MediatR;
using Microsoft.Extensions.Logging;
using Shopify.Domain.Entities;
using Shopify.Domain.Repositories;

namespace Shopify.Application.UseCases.Orders.Queries.OrderQuery
{
    public class OrderQueryHandler : IRequestHandler<OrderQuery, Order>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILogger<OrderQueryHandler> _logger;
        public OrderQueryHandler(IRepositoryManager repositoryManager, ILogger<OrderQueryHandler> logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }
        public async Task<Order> Handle(OrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _repositoryManager.OrderRepository.GetOrderByIdAsync(request.Id, false);
            if (order == null)
            {
                _logger.LogWarning("Order with ID {OrderId} not found.", request.Id);
                throw new KeyNotFoundException($"Order with ID {request.Id} not found.");
            }
            _logger.LogInformation("Order with ID {OrderId} retrieved successfully.", request.Id);
            return order;
        }
    }
}

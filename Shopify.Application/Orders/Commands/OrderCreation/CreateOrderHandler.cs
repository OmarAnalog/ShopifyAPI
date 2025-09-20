using MediatR;
using Microsoft.Extensions.Logging;
using Shopify.Application.Services;
using Shopify.Domain.Entities;
using Shopify.Domain.Repositories;

namespace Shopify.Application.Orders.Commands.OrderCreation
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IPaymentService _paymentService;
        private readonly ILogger<CreateOrderHandler> _logger;

        public CreateOrderHandler(IRepositoryManager repositoryManager,
                                  IPaymentService paymentService,
                                  ILogger<CreateOrderHandler> logger)
        {
            _repositoryManager = repositoryManager;
            _paymentService = paymentService;
            _logger = logger;
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            if (request.OrderDto is null || !request.OrderDto.OrderItems.Any())
                throw new ArgumentNullException("Order must contain at least one item.");
            // Logic to create order in the system would go here.
            // first create order
            var order = new Order()
            {
                CustomerId = request.OrderDto.CustomerId,
                TotalAmount = 0,
            };
            await CalcOrderItems(request, order);
            var paymentResult=await _paymentService.ProcessPayment(order.TotalAmount,
                                                                   request.OrderDto.Payment.Method);
            if (!paymentResult.Success)
            {
                _logger.LogWarning("Payment failed for Customer {CustomerId}: {Message}",
                                   order.CustomerId, paymentResult.Message);
            }

            await _repositoryManager.BeginTransaction();

            try
            {
                await _repositoryManager.OrderRepository.CreateOrderAsync(order);
                await _repositoryManager.SaveAsync();
                await _repositoryManager.CommitTransaction();
            }
            catch
            {
                await _repositoryManager.RollbackTransaction();
                throw;
            }
            _logger.LogInformation("Order {OrderId} created " +
                "successfully for Customer {CustomerId} with Total Amount {TotalAmount}",
                                   order.Id, order.CustomerId, order.TotalAmount);
            return order.Id;
        }

        private async Task CalcOrderItems(CreateOrderCommand request, Order order)
        {
            foreach (var item in request.OrderDto.OrderItems)
            {
                var product = await _repositoryManager.ProductRepository.GetProductByIdAsync(item.ProductId, false);
                if (product is null)
                    throw new ArgumentException($"Product with ID {item.ProductId} not found.");
                var unitPrice = product.Price;
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = unitPrice
                });
                order.TotalAmount += unitPrice * item.Quantity;
            }
        }
    }
}

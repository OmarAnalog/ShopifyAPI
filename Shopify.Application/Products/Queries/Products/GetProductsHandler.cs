using MediatR;
using Microsoft.Extensions.Logging;
using Shopify.Application.Products.Queries.Dtos;
using Shopify.Domain.Repositories;

namespace Shopify.Application.Products.Queries.Products
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILogger<GetProductsHandler> _logger;
        public GetProductsHandler(IRepositoryManager repositoryManager, ILogger<GetProductsHandler> logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }
        public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _repositoryManager.ProductRepository.GetAllProductsAsync(false);
            // we will change it later when we use mapster
            var productsDto = products.Select(ProductDto.FactoryProduct).ToList();
            _logger.LogInformation("Returned {Count} products successfully", productsDto.Count);
            return productsDto;
        }
    }
}

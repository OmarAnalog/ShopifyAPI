using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopify.Application.UseCases.Products.Queries.Products;
using Shopify.Infrastructure.Persistence;

namespace Shopify.Presentation.Controllers
{
    [ApiController]
    [Route("api/Products")]
    public class ProductsController : ControllerBase
    {
        private readonly ISender _mediatR;

        public ProductsController(ISender mediatr)
        {
            _mediatR = mediatr;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products =  await _mediatR.Send(new GetProductsQuery());
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            return Ok();
        }
    }
}

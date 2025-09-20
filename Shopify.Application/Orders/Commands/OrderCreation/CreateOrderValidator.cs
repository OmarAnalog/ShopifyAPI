using FluentValidation;
using Shopify.Application.Orders.Dtos;

namespace Shopify.Application.Orders.Commands.OrderCreation
{
    public class CreateOrderValidator:AbstractValidator<CreateOrderDto>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.OrderItems).NotEmpty();
            RuleForEach(x => x.OrderItems).ChildRules(i =>
            {
                i.RuleFor(x => x.ProductId).GreaterThan(0);
                i.RuleFor(x => x.Quantity).GreaterThan(0);
            });
            RuleFor(x => x.Payment).NotNull();
            RuleFor(x => x.Payment.Amount).GreaterThan(0);
        }
    }
}

namespace Shopify.Application.Orders.Dtos
{
    public record CreateOrderDto
    (string CustomerId,
    List<OrderItemDto> OrderItems,
    PaymentDto Payment);
    public record OrderItemDto(int ProductId, int Quantity);
    public record PaymentDto(decimal Amount,string Method= "fake");
}

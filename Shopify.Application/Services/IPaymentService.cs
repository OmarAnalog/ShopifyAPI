namespace Shopify.Application.Services
{
    public record PaymentResult(bool Success, string? Message = "");
    public interface IPaymentService
    {
        Task<PaymentResult> ProcessPayment(decimal amount, string method);
    }
}

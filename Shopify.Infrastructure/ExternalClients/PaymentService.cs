using Shopify.Application.Services;

namespace Shopify.Infrastructure.ExternalClients
{
    internal class PaymentService : IPaymentService
    {
        public Task<PaymentResult> ProcessPayment(decimal amount, string method)
        {
            return Task.FromResult(new PaymentResult(true, "Payment processed successfully."));
        }
    }
}

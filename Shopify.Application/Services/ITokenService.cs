namespace Shopify.Application.Services
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(string userId);
    }
}

using Shopify.Domain.Dtos.Authentication;

namespace Shopify.Domain.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<AuthResult> Register(RegisterDto registerDto);
        Task<AuthResult> Login(string email, string password);
    }
}

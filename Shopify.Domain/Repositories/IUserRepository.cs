using ErrorOr;
using Shopify.Domain.Dtos.Authentication;

namespace Shopify.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<ErrorOr<AuthResult>> Register(RegisterDto registerDto);
        Task<AuthResult> Login(string email, string password);
    }
}

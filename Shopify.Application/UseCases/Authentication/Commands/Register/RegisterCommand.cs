using ErrorOr;
using MediatR;
using Shopify.Domain.Dtos.Authentication;

namespace Shopify.Application.UseCases.Authentication.Commands.Register
{
    public record RegisterCommand(string FirstName, string LastName, string UserName, string Email, string Password, IEnumerable<string>? Roles)
        : IRequest<ErrorOr<AuthResult>>
    {
        public static RegisterCommand Create(RegisterDto request)
        {
            return new RegisterCommand(request.FirstName, request.LastName, request.UserName, request.Email, request.Password, request.Roles);
        }
    }
}

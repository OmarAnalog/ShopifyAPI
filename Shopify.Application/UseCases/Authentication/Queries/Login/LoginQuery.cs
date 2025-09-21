using MediatR;
using Shopify.Domain.Dtos.Authentication;

namespace Shopify.Application.UseCases.Authentication.Queries.Login
{
    public record LoginQuery(string Email, string Password):IRequest<AuthResult>;
}

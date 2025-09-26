using MediatR;
using Shopify.Domain.Dtos.Authentication;
using Shopify.Domain.Repositories;

namespace Shopify.Application.UseCases.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthResult>
    {
        private readonly IRepositoryManager _repositoryManager;

        public LoginQueryHandler(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public Task<AuthResult> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var authResult = _repositoryManager.UserRepository.Login(request.Email, request.Password);
            if (authResult == null)
            {
                throw new Exception("Invalid email or password");
            }
            return authResult;
        }
    }
}

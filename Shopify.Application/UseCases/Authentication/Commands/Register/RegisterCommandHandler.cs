using MediatR;
using Microsoft.Extensions.Logging;
using Shopify.Domain.Dtos.Authentication;
using Shopify.Domain.Repositories;

namespace Shopify.Application.UseCases.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResult>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILogger<RegisterCommandHandler> _logger;
        public RegisterCommandHandler(IRepositoryManager repositoryManager, ILogger<RegisterCommandHandler> logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }
        public async Task<AuthResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // we will not deal with user manager here we will only send the data to the identity service
            // and the identity service will deal with user manager
            var registerDto = new RegisterDto()
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = request.Password,
                Roles = request.Roles
            };
            var authResult = await _repositoryManager.UserRepository.Register(registerDto);
            if (authResult == null)
            {
                _logger.LogError("User registration failed for {UserName}", request.UserName);
                throw new Exception("User registration failed");
            }
            _logger.LogInformation("User registered successfully: {UserName}", request.UserName);
            var AuthResult = new AuthResult
            {
                UserId = authResult.UserId,
                UserName = authResult.UserName,
                FirstName = authResult.FirstName,
                Token = authResult.Token
            };
            return AuthResult;
        }
    }
}

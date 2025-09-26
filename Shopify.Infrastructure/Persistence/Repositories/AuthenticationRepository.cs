using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Shopify.Application.Services;
using Shopify.Domain.Common.Errors;
using Shopify.Domain.Dtos.Authentication;
using Shopify.Domain.Entities.Identity;
using Shopify.Domain.Repositories;

namespace Shopify.Infrastructure.Persistence.Repositories
{
    public class AuthenticationRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        public AuthenticationRepository(UserManager<User> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<AuthResult> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
            if (!isPasswordValid)
            {
                throw new Exception("InvalidCreadentials");
            }
            return new AuthResult
            {
                UserId = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                Token = await _tokenService.CreateTokenAsync(user.Id)
            };
        }

        public async Task<ErrorOr<AuthResult>> Register(RegisterDto registerDto)
        {
            if (await _userManager.FindByEmailAsync(registerDto.Email) != null)
            {
                return Errors.User.DuplicateEmail;
            }
            if (await _userManager.FindByNameAsync(registerDto.UserName) != null)
            {
                return Errors.User.DuplicateEmail;
            }
            var user = new User
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                // we can add roles here if needed
                return new AuthResult
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    Token = await _tokenService.CreateTokenAsync(user.Id)
                };
            }
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));// we can create custom exception here
        }
    }
}

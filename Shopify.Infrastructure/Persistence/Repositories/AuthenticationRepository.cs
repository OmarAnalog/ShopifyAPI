using Microsoft.AspNetCore.Identity;
using Shopify.Application.Services;
using Shopify.Domain.Dtos.Authentication;
using Shopify.Domain.Repositories;
using Shopify.Infrastructure.Identity;

namespace Shopify.Infrastructure.Persistence.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        public AuthenticationRepository(UserManager<User> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<AuthResult?> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return null;
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
            if (!isPasswordValid)
            {
                return null;
            }
            return new AuthResult
            {
                UserId = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                Token = await _tokenService.CreateTokenAsync(user.Id)
            };
        }

        public async Task<AuthResult?> Register(RegisterDto registerDto)
        {
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
            return null;
        }
    }
}

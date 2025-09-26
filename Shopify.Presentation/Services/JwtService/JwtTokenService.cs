using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shopify.Application.Services;
using Shopify.Domain.Entities.Identity;
using Shopify.Presentation.Services.JwtService.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shopify.Presentation.Services.JwtService
{
    public class JwtTokenService : ITokenService
    {
        private readonly IOptions<JwtConfiguration> options;
        private readonly JwtConfiguration jwtConfiguration;
        private readonly UserManager<User> _userManager;
        private User? _user;
        public JwtTokenService(IOptions<JwtConfiguration> options,
                               UserManager<User> userManager)
        {
            this.options = options;
            jwtConfiguration = options.Value;
            _userManager = userManager;
        }
        public async Task<string> CreateTokenAsync(string userId)
        {
            /*
             1- siginig credentials
             2-claims
             3-token options
             
             */
            _user = await _userManager.FindByIdAsync(userId);
            if (_user == null) throw new Exception("User not found");
            var signingCredentials = GetSigningCredentials();
            var claims = GetClaimsAsync(userId);
            var tokenOptions = GetTokenOptions(signingCredentials, await claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(jwtConfiguration.Secret);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret,SecurityAlgorithms.HmacSha256);
        }
        private async Task<List<Claim>> GetClaimsAsync(string userId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,userId),
                new Claim(ClaimTypes.Name,_user.UserName),
                new Claim(ClaimTypes.Email,_user.Email)
            };
            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
        private JwtSecurityToken GetTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: jwtConfiguration.Issuer,
                audience: jwtConfiguration.Audiance,
                claims: claims,
                expires: DateTime.Now.AddMinutes(jwtConfiguration.Expiry),
                signingCredentials: signingCredentials);
            return tokenOptions;
        }
    }
}

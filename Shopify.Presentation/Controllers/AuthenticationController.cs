using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shopify.Application.Services;
using Shopify.Application.UseCases.Authentication.Commands.Register;
using Shopify.Domain.Dtos.Authentication;
using Shopify.Infrastructure.Identity;
using Shopify.Presentation.Services.JwtService;

namespace Shopify.Presentation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController:ControllerBase
    {
        private readonly ISender _mediatr;

        public AuthenticationController(ISender mediatr)
        {
            _mediatr = mediatr;
        }

        // Implement authentication endpoints (e.g., login, register) here

        [HttpPost("login")]
        public IActionResult Login()
        {
            // Placeholder for login logic
            return Ok("Login endpoint");
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto request)
        {
            var command = new RegisterCommand(request.FirstName,request.LastName,request.UserName,request.Email,request.Password,request.Roles);
            var authResponse = await _mediatr.Send(command);
            // Placeholder for register logic
            return Ok(authResponse);
        }
    }
}

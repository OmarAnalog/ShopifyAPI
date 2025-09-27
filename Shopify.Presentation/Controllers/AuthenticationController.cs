using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shopify.Application.Services;
using Shopify.Application.UseCases.Authentication.Commands.Register;
using Shopify.Domain.Dtos.Authentication;
using Shopify.Presentation.Services.JwtService;

namespace Shopify.Presentation.Controllers
{
    [Route("api/auth")]
    public class AuthenticationController:ApiController
    {
        private readonly ISender _mediatr;
        private readonly IMapper _mapper;

        public AuthenticationController(ISender mediatr, IMapper mapper)
        {
            _mediatr = mediatr;
            _mapper = mapper;
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
            var command = _mapper.Map<RegisterCommand>(request);
            ErrorOr<AuthResult> authResponse = await _mediatr.Send(command);
            // Placeholder for register logic
            return authResponse.Match(
                authResult => Ok(authResult),
                errors => Problem(errors)
            );
        }
    }
}

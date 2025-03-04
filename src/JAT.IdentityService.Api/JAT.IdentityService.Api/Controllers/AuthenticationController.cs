using JAT.Core.Api;
using JAT.IdentityService.Api.Contracts.Authentication.Login;
using JAT.IdentityService.Application.Authentication.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JAT.IdentityService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IMediator mediator) : ApiController
    {
        private readonly IMediator _mediator = mediator;

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _mediator.Send(MapToCommand(request));
            return result.Match(value => Ok(MapToResponse(value)), Problem);
        }

        private static LoginResponse MapToResponse(LoginCommandResult value)
        {
            return new (value.Token, value.RefreshToken);
        }

        private static LoginCommand MapToCommand(LoginRequest request)
        {
            return new (request.Username, request.Password);
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JAT.IdentityService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
}

using JAT.Core.Api;
using JAT.IdentityService.Api.Contracts.Users.Create;
using JAT.IdentityService.Application.Users.Create;
using JAT.IdentityService.Application.Users.List;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JAT.IdentityService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IMediator mediator) : ApiController
{
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Returns all the users
    /// </summary>
    /// <returns>All the users</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _mediator.Send(new ListUsersQuery());
        return result.Match(value => Ok(value.Select(MapToUserDTO)), Problem);
    }

    private static UserDTO MapToUserDTO(UserDTO userDto)
    {
        ArgumentNullException.ThrowIfNull(userDto);
        return new (userDto.Id, userDto.Username, userDto.Email, userDto.Role, userDto.Status);
    }

    /// <summary>
    /// Creates a new user (defaults its status to Active and its role to User)
    /// </summary>
    /// <param name="request">The user's information</param>
    /// <returns>The Id of the user</returns>
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var result = await _mediator.Send(MapToCreateUserCommand(request));
        return result.Match(value =>
            CreatedAtAction(nameof(GetAllUsers), new { id = value.Id }, MapToResponse(value)), Problem);
    }

    private static CreateUserResponse MapToResponse(CreateUserResult result)
    {
        ArgumentNullException.ThrowIfNull(result);
        return new (result.Id);
    }

    private static CreateUserCommand MapToCreateUserCommand(CreateUserRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        return new (request.Username, request.Email, request.PasswordHash);
    }
}

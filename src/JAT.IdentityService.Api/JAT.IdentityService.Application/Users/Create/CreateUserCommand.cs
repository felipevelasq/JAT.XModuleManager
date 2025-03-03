using JAT.Core.Domain.Commons.Results;
using MediatR;

namespace JAT.IdentityService.Application.Users.Create;

public record CreateUserCommand(
    string Username,
    string Email,
    string PasswordHash)
 : IRequest<Result<CreateUserResult>>;

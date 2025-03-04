using JAT.Core.Domain.Commons.Results;
using MediatR;

namespace JAT.IdentityService.Application.Authentication.Login;

public record LoginCommand(string Username, string Password) : IRequest<Result<LoginCommandResult>>;

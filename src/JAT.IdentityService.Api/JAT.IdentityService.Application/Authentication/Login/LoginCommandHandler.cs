using System.Linq.Expressions;
using JAT.Core.Domain.Commons.Results;
using JAT.IdentityService.Domain.Interfaces.Repositories;
using JAT.IdentityService.Domain.Interfaces.Services;
using JAT.IdentityService.Domain.Users;
using MediatR;

namespace JAT.IdentityService.Application.Authentication.Login;

public class LoginCommandHandler(
    IUserRepository userRepository,
    IPasswordService passwordService,
    ITokenService tokenService)
    : IRequestHandler<LoginCommand, Result<LoginCommandResult>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordService _passwordService = passwordService;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<Result<LoginCommandResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        Expression<Func<User, bool>> userExists = user => user.Username == request.Username;

        var getAsyncResult = await _userRepository.GetAsync(userExists, cancellationToken);
        var wasUserFound = !getAsyncResult.Any();

        var userFound = getAsyncResult?.FirstOrDefault();

        if (wasUserFound || userFound!.Deleted)
        {
            return UserError.InvalidCredentials;
        }

        var saltedHashedPassword = _passwordService.HashPassword(request.Password, userFound.Salt);

        if (userFound.PasswordHash != saltedHashedPassword)
        {
            return UserError.InvalidCredentials;
        }

        return MapToCommandResult(_tokenService.GenerateToken(userFound), _tokenService.GenerateRefreshToken());
    }

    private static LoginCommandResult MapToCommandResult(string token, string refreshToken)
    {
        return new LoginCommandResult(token, refreshToken);
    }
}

using System.Linq.Expressions;
using JAT.Core.Domain.Commons.Results;
using JAT.Core.Domain.Enums;
using JAT.Core.Domain.Interfaces;
using JAT.IdentityService.Domain.Interfaces.Repositories;
using JAT.IdentityService.Domain.Interfaces.Services;
using JAT.IdentityService.Domain.Users;
using MediatR;

namespace JAT.IdentityService.Application.Users.Create;

public class CreateUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IPasswordService passwordService)
    : IRequestHandler<CreateUserCommand, Result<CreateUserResult>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPasswordService _passwordService = passwordService;

    public async Task<Result<CreateUserResult>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        Expression<Func<User, bool>> expression = user => user.Username == request.Username || user.Email == request.Email;

        if (await _userRepository.ExistAsync(expression, cancellationToken))
        {
            return UserError.UserAlreadyExists;
        }

        var salt = _passwordService.GenerateSalt();
        var hashedSaltedPassword = _passwordService.HashPassword(request.Password, salt);

        var newUser = MapToUser(request, hashedSaltedPassword, salt);
        
        await _userRepository.AddAsync(newUser, cancellationToken);

        if (!await _unitOfWork.CommitAsync(cancellationToken))
        {
            return Error.Conflict("User could not be created");
        }

        return MapToCreateUserResult(newUser);
    }

    public static User MapToUser(CreateUserCommand command, string hashedSaltedPassword, byte[] salt)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        return new User(
            Guid.NewGuid(),
            command.Username,
            command.Email,
            hashedSaltedPassword,
            UserRoleType.User,
            salt);
    }

    public static CreateUserResult MapToCreateUserResult(User user)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));
        return new CreateUserResult(user.Id);
    }
}
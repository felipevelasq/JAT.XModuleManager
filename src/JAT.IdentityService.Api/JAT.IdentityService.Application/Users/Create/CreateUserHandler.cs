using JAT.Core.Domain.Commons.Results;
using JAT.Core.Domain.Enums;
using JAT.Core.Domain.Interfaces;
using JAT.IdentityService.Domain;
using JAT.IdentityService.Domain.Interfaces.Repositories;
using MediatR;

namespace JAT.IdentityService.Application.Users.Create;

public class CreateUsersHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateUserCommand, Result<CreateUserResult>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<CreateUserResult>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var newUser = MapToUser(request);
        
        await _userRepository.AddAsync(newUser, cancellationToken);

        if (!await _unitOfWork.CommitAsync(cancellationToken))
        {
            return Error.Conflict("User could not be created");
        }

        return MapToCreateUserResult(newUser);
    }

    public static User MapToUser(CreateUserCommand command)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        return new User(
            Guid.NewGuid(),
            command.Username,
            command.Email,
            command.PasswordHash,
            UserRoleType.User);
    }

    public static CreateUserResult MapToCreateUserResult(User user)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));
        return new CreateUserResult(user.Id);
    }
}
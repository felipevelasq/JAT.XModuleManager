using JAT.Core.Domain.Commons.Results;
using JAT.IdentityService.Domain.Interfaces.Repositories;
using JAT.IdentityService.Domain.Users;
using MediatR;

namespace JAT.IdentityService.Application.Users.List;

public class ListUsersQueryHandler(IUserRepository userRepository)
    : IRequestHandler<ListUsersQuery, Result<IEnumerable<UserDTO>>>
{
    private readonly IUserRepository _userRepository = userRepository;

    /// <summary>
    /// Handles the request to list users.
    /// </summary>
    /// <param name="request">The request to list users.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A result containing a list of UserDTOs.</returns>
    public async Task<Result<IEnumerable<UserDTO>>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);
        var userDTOs = users.Select(MapToDTO).ToArray();
        return userDTOs;
    }

    public static UserDTO MapToDTO(User user)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));
        return new UserDTO(user.Id, user.Username, user.Email, user.Role, user.Status);
    }
}
using JAT.Core.Domain.Commons.Results;
using JAT.IdentityService.Domain;
using JAT.IdentityService.Domain.Interfaces.Repositories;
using MediatR;

namespace JAT.IdentityService.Application.Users.List;

public class ListUsersHandler(IUserRepository userRepository)
    : IRequestHandler<ListUsersQuery, Result<IEnumerable<UserDTO>>>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result<IEnumerable<UserDTO>>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
    {
        var users = _userRepository.GetAllAsync();
        var userDTOs = users.Select(x => MapToDTO(x)).ToArray();
        return await Task.FromResult(userDTOs);
    }

    public static UserDTO MapToDTO(User user)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));
        return new UserDTO(user.Id, user.Username, user.Email, user.Role, user.Status);
    }
}
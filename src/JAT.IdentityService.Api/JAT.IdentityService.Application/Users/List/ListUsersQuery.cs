using JAT.Core.Domain.Commons.Results;
using MediatR;

namespace JAT.IdentityService.Application.Users.List;

public record ListUsersQuery : IRequest<Result<IEnumerable<UserDTO>>>;
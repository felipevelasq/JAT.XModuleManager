using JAT.Core.Domain.Enums;

namespace JAT.IdentityService.Application.Users.List;

public record UserDTO(
    Guid Id,
    string Username,
    string Email,
    UserRoleType Role,
    UserStatusType Status
);

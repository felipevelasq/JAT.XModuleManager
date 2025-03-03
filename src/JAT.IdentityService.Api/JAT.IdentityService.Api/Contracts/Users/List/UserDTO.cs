using JAT.Core.Domain.Enums;

namespace JAT.IdentityService.Api.Contracts.Users.List;

public record UserDTO(
    Guid Id,
    string Username,
    string Email,
    UserRoleType Role,
    UserStatusType Status
);

using JAT.Core.Domain.Entities;
using JAT.Core.Domain.Enums;

namespace JAT.IdentityService.Domain.Users;

public class User : Entity
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; } = null;
    public UserRoleType Role { get; set; }
    public byte[] Salt { get; private set; }
    public UserStatusType Status { get; set; }

    public User(
        Guid id,
        string username,
        string email,
        string passwordHash,
        UserRoleType role,
        byte[] salt,
        UserStatusType status = UserStatusType.Active
    ) : base(id)
    {
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        Salt = salt;
        Status = status;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }
}
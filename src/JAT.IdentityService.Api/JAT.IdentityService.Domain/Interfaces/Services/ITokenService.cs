using JAT.IdentityService.Domain.Users;

namespace JAT.IdentityService.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        string GenerateRefreshToken();
    }
}
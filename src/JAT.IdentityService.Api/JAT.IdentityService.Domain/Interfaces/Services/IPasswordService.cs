namespace JAT.IdentityService.Domain.Interfaces.Services;

public interface IPasswordService
{
    byte[] GenerateSalt();
    string HashPassword(string password, byte[] salt);    
}

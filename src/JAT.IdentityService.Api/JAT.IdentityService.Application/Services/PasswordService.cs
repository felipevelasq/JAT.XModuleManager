using System.Security.Cryptography;
using JAT.IdentityService.Domain.Constants;
using JAT.IdentityService.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace JAT.IdentityService.Application.Services;

public class PasswordService : IPasswordService
{
    public byte[] GenerateSalt()
    {
        var salt = new byte[MaxLengths.Password.Salt];
        using (var randomNumberGenerator = RandomNumberGenerator.Create())
        {
            randomNumberGenerator.GetBytes(salt);
        }
        return salt;
    }

    public string HashPassword(string password, byte[] salt)
    {
        var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 32));
        return hashed;
    }
}

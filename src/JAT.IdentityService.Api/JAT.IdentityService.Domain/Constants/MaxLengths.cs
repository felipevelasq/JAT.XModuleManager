namespace JAT.IdentityService.Domain.Constants;

public static class MaxLengths
{
    public static class User
    {
        public const int Username = 50;
        public const int Email = 100;
        public const int PasswordHash = 100;
    }

    public static class Password
    {
        public const int Salt = 16;
    }
}

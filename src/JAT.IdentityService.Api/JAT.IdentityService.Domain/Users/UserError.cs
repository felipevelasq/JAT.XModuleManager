using JAT.Core.Domain.Commons.Results;

namespace JAT.IdentityService.Domain.Users;

public static class UserError
{
    public static Error UserAlreadyExists => Error.Validation(
        code: nameof(UserAlreadyExists),
        message: "User already exists");
        
    public static Error UserCouldNotBeCreated => Error.Conflict(
        code: nameof(UserCouldNotBeCreated),
        message: "User could not be created");

    public static Error UserNotFound => Error.NotFound(
        code: "UserNotFound",
        message: "User not found");

    public static Error InvalidCredentials => Error.Validation(
        code: "InvalidCredentials",
        message: "Invalid credentials!");
}

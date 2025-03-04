namespace JAT.IdentityService.Api.Contracts.Users.Create;

public record CreateUserRequest(
    string Username,
    string Email,
    string Password);

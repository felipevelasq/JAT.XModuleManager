namespace JAT.IdentityService.Application.Authentication.Login;

public record LoginCommandResult(string Token, string RefreshToken);

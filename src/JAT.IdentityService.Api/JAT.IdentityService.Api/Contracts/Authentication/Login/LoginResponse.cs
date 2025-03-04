namespace JAT.IdentityService.Api.Contracts.Authentication.Login;

public record LoginResponse(string Token, string RefreshToken);

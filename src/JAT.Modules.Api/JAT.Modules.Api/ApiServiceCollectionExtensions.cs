using System.Reflection;
using System.Text;
using JAT.Modules.Application;
using JAT.Core.Domain.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace JAT.Modules.Api;

public static class ApiServiceCollectionExtensions
{
    public static IServiceCollection AddMediatrConfigs(this IServiceCollection services)
    {
        var mediatRAssemblies = new[]
        {
            Assembly.GetAssembly(typeof(ApplicationServiceCollectionExtensions)),
        };
        
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!));

        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(jwtOptions =>
            {
                jwtOptions.TokenValidationParameters = CreateTokenValidationParameters(configuration);
            });
        
        services
            .AddOptions<TokenSettings>()
            .Bind(configuration.GetSection("Auth"))
            .ValidateOnStart();

        return services;
    }

    private static TokenValidationParameters CreateTokenValidationParameters(IConfiguration configuration)
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Auth:Issuer"],
            ValidAudience = configuration["Auth:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    configuration["Auth:Secret"]!)),
            RequireSignedTokens = false,
        };
    }
}

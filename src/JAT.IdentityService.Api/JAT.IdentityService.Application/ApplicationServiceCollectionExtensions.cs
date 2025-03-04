using FluentValidation;
using JAT.Core.Application;
using JAT.IdentityService.Application.Services;
using JAT.IdentityService.Domain.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace JAT.IdentityService.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Services
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPasswordService, PasswordService>();

        services.AddValidatorsFromAssemblyContaining(typeof(ApplicationServiceCollectionExtensions));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        
        return services;
    }
}

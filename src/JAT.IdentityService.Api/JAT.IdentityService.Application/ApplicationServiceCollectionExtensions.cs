using FluentValidation;
using JAT.Core.Application;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace JAT.IdentityService.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining(typeof(ApplicationServiceCollectionExtensions));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        
        return services;
    }
}

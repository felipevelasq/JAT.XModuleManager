using System.Reflection;
using JAT.IdentityService.Application;

namespace JAT.IdentityService.Api;

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
}

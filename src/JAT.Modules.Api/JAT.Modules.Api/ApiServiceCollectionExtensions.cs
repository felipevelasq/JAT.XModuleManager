using System.Reflection;
using JAT.Modules.Application;
using JAT.Modules.Infrastructure;

namespace JAT.Modules.Api;

public static class ApiServiceCollectionExtensions
{
    public static IServiceCollection AddMediatrConfigs(this IServiceCollection services)
    {
        var mediatRAssemblies = new[]
        {
            Assembly.GetAssembly(typeof(ApplicationServiceCollectionExtensions)),
            Assembly.GetAssembly(typeof(InfrastructureServiceCollectionExtensions))
        };
        
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!));

        return services;
    }
}

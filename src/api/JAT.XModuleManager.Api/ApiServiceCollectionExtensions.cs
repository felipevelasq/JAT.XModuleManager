using System.Reflection;
using JAT.XModuleManager.Application;
using JAT.XModuleManager.Infrastructure;

namespace JAT.XModuleManager.Api;

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

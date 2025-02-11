using JAT.XModuleManager.Domain;
using JAT.XModuleManager.Domain.Interfaces;
using JAT.XModuleManager.Infrastructure.Persistance.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace JAT.XModuleManager.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<StaticDataContext<Module>>();
        services.AddScoped<IModuleRepository, ModuleRepository>();
        return services;
    }
}

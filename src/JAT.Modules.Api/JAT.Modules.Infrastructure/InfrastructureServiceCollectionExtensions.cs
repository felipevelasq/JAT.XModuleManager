using JAT.Modules.Domain;
using JAT.Modules.Domain.Interfaces;
using JAT.Modules.Infrastructure.Persistance.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace JAT.Modules.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<StaticDataContext<Module>>();
        services.AddScoped<IModuleRepository, ModuleRepository>();
        return services;
    }
}

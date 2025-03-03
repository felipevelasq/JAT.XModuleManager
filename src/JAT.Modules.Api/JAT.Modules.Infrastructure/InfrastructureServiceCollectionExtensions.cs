using JAT.Core.Domain.Interfaces;
using JAT.Modules.Domain;
using JAT.Modules.Domain.Interfaces;
using JAT.Modules.Infrastructure.Persistance.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JAT.Modules.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddDbContext<ModulesDbContext>(
        //     options =>
        //     {
        //         options.UseSqlite(configuration.GetConnectionString(nameof(ModulesDbContext))); // $"Data Source={nameof(ModulesDbContext)}.db"
        //     });
        
        // services.AddScoped<IUnitOfWork, UnitOfWork<ModulesDbContext>>();

        services.AddSingleton<StaticDataContext<Module>>();
        services.AddScoped<IModuleRepository, ModuleRepository>();
        return services;
    }
}

using JAT.Core.Domain.Interfaces;
using JAT.IdentityService.Domain.Interfaces.Repositories;
using JAT.IdentityService.Domain.Interfaces.Services;
using JAT.IdentityService.Infrastructure.Database;
using JAT.IdentityService.Infrastructure.Extensions;
using JAT.IdentityService.Infrastructure.Repositories;
using JAT.IdentityService.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JAT.IdentityService.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityServiceDbContext>(
            options =>
            {
                options.UseSqlite(
                    configuration.GetConnectionString(nameof(IdentityServiceDbContext))); // $"Data Source={nameof(IdentityServiceDbContext)}.db"
            });
        
        services.AddScoped<IUnitOfWork, UnitOfWork<IdentityServiceDbContext>>();

        // Services
        services.AddScoped<IPasswordService, PasswordService>();

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }

    public static IServiceProvider ApplyContextUpdates(this IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var services = scope.ServiceProvider;
            var appDbContext = services.GetRequiredService<IdentityServiceDbContext>();
            appDbContext.EnsureMigrationsApplied();
        }
        return serviceProvider;
    }
}
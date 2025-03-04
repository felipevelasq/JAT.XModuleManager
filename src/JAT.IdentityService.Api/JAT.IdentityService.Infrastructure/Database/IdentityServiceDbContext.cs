using JAT.IdentityService.Domain.Users;
using JAT.IdentityService.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace JAT.IdentityService.Infrastructure.Database;

public class IdentityServiceDbContext(DbContextOptions<IdentityServiceDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ApplyConfigurations(modelBuilder);
    }

    private static void ApplyConfigurations(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
    }
}
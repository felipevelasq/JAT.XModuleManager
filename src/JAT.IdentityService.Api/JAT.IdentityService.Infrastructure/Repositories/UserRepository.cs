using JAT.IdentityService.Domain;
using JAT.IdentityService.Domain.Interfaces.Repositories;
using JAT.IdentityService.Infrastructure.Database;

namespace JAT.IdentityService.Infrastructure.Repositories;

public sealed class UserRepository(IdentityServiceDbContext context)
 : DbBaseRepository<User>(context), IUserRepository
{
}

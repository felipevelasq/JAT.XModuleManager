
namespace JAT.XModuleManager.Domain.Interfaces;

public interface IModuleRepository
{
    Task AddAsync(Module module, CancellationToken cancellationToken);
    Task<IEnumerable<Module>> GetAllAsync(CancellationToken cancellationToken);
}

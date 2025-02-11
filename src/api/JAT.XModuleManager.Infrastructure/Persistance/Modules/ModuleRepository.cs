﻿using JAT.XModuleManager.Domain;
using JAT.XModuleManager.Domain.Interfaces;

namespace JAT.XModuleManager.Infrastructure.Persistance.Modules;

public class ModuleRepository(StaticDataContext<Module> staticDataContext)
 : IModuleRepository
{
    private readonly StaticDataContext<Module> _staticDataContext = staticDataContext;

    public async Task AddAsync(Module module, CancellationToken cancellationToken)
    {
        await _staticDataContext.AddAsync(module, cancellationToken);
    }

    public async Task<IEnumerable<Module>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _staticDataContext.GetAllAsync(cancellationToken);
    }
}

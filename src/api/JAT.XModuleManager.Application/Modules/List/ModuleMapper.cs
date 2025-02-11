using JAT.XModuleManager.Domain;

namespace JAT.XModuleManager.Application.Modules.List;

public static partial class ModuleMapper
{
    public static ModuleDTO MapToDTO(this Module module)
    {
        ArgumentNullException.ThrowIfNull(module, nameof(module));
        return new ModuleDTO(module.Id, module.Name, module.Description, module.ModuleType);
    }
}

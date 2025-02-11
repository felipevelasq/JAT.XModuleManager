namespace JAT.XModuleManager.Api.Contracts.Modules.List;

public static partial class ModuleMapper
{
    public static ModuleDTO MapToDTO(this Application.Modules.List.ModuleDTO moduleDto)
    {
        ArgumentNullException.ThrowIfNull(moduleDto);
        return new (moduleDto.Id, moduleDto.Name, moduleDto.Description, moduleDto.ModuleType);
    }
}

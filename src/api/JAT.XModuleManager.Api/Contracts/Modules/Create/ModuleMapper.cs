using JAT.XModuleManager.Application.Modules.Create;

namespace JAT.XModuleManager.Api.Contracts.Modules.Create;

public static partial class ModuleMapper
{
    public static CreateModuleResponse MapToResponse(this CreateModuleResult result)
    {
        ArgumentNullException.ThrowIfNull(result);
        return new (result.Id);
    }

    public static CreateModuleCommand MapToCommand(this CreateModuleRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        return new (request.Name, request.Description, request.ModuleType);
    }
}

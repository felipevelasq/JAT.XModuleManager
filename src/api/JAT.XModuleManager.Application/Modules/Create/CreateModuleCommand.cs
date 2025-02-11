using MediatR;

namespace JAT.XModuleManager.Application.Modules.Create;

public record CreateModuleCommand(string Name, string? Description, string ModuleType) : IRequest<CreateModuleResult>;
using MediatR;

namespace JAT.Modules.Application.Modules.Create;

public record CreateModuleCommand(string Name, string? Description, string ModuleType) : IRequest<CreateModuleResult>;
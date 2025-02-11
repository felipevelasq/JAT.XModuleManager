using MediatR;

namespace JAT.XModuleManager.Application.Modules.List;

public record ListModulesQuery : IRequest<IEnumerable<ModuleDTO>>;
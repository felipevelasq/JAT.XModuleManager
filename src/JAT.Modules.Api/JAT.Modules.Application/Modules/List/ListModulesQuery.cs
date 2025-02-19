using MediatR;

namespace JAT.Modules.Application.Modules.List;

public record ListModulesQuery : IRequest<IEnumerable<ModuleDTO>>;
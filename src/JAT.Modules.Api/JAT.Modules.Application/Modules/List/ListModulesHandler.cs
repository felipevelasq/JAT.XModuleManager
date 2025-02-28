using JAT.Core.Domain.Commons.Results;
using JAT.Modules.Domain.Interfaces;
using MediatR;

namespace JAT.Modules.Application.Modules.List;

public class ListModulesHandler(IModuleRepository moduleRepository)
    : IRequestHandler<ListModulesQuery, Result<IEnumerable<ModuleDTO>>>
{
    private readonly IModuleRepository _moduleRepository = moduleRepository;

    public async Task<Result<IEnumerable<ModuleDTO>>> Handle(ListModulesQuery request, CancellationToken cancellationToken)
    {
        var modules = await _moduleRepository.GetAllAsync(cancellationToken);
        var moduleDTOs = modules.Select(x => x.MapToDTO()).ToArray();
        return moduleDTOs;
    }
}
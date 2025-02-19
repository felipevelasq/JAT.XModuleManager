using JAT.Modules.Domain.Interfaces;
using MediatR;

namespace JAT.Modules.Application.Modules.List;

public class ListModulesHandler(IModuleRepository moduleRepository)
    : IRequestHandler<ListModulesQuery, IEnumerable<ModuleDTO>>
{
    private readonly IModuleRepository _moduleRepository = moduleRepository;

    public async Task<IEnumerable<ModuleDTO>> Handle(ListModulesQuery request, CancellationToken cancellationToken)
    {
        var result = await _moduleRepository.GetAllAsync(cancellationToken);
        return result.Select(x => x.MapToDTO());
    }
}
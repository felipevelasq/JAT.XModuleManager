using JAT.Modules.Domain;
using JAT.Modules.Domain.Interfaces;
using MediatR;

namespace JAT.Modules.Application.Modules.Create;

public class CreateModuleHandler(IModuleRepository moduleRepository)
 : IRequestHandler<CreateModuleCommand, CreateModuleResult>
{
    private readonly IModuleRepository _moduleRepository = moduleRepository;

    public async Task<CreateModuleResult> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
    {
        var module = new Module(request.Name, request.Description, request.ModuleType);
        await _moduleRepository.AddAsync(module, cancellationToken);
        return new CreateModuleResult(module.Id);
    }
}
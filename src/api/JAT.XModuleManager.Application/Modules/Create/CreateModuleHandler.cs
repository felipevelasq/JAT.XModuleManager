using JAT.XModuleManager.Domain;
using JAT.XModuleManager.Domain.Interfaces;
using MediatR;

namespace JAT.XModuleManager.Application.Modules.Create;

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
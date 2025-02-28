using JAT.Core.Domain.Commons.Results;
using MediatR;

namespace JAT.Modules.Application.Modules.Create;

public record CreateModuleCommand(string Name, string? Description, string ModuleType)
 : IRequest<Result<CreateModuleResult>>;
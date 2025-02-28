using JAT.Core.Domain.Commons.Results;
using MediatR;

namespace JAT.Modules.Application.Modules.List;

public record ListModulesQuery : IRequest<Result<IEnumerable<ModuleDTO>>>;
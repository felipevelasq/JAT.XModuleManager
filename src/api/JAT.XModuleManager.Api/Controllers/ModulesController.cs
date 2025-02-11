using JAT.XModuleManager.Api.Contracts.Modules.Create;
using JAT.XModuleManager.Api.Contracts.Modules.List;
using JAT.XModuleManager.Application.Modules.List;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JAT.XModuleManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ModulesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    // GET: api/<ModulesController>
    [HttpGet]
    public async Task<IEnumerable<Contracts.Modules.List.ModuleDTO>> Get()
    {
        var result = await _mediator.Send(new ListModulesQuery());
        return result.Select(x => x.MapToDTO());
    }

    // GET api/<ModulesController>/5
    [HttpGet("{id}")]
    public Task<IActionResult> Get(int id)
    {
        return Task.FromResult<IActionResult>(NotFound());
    }

    // POST api/<ModulesController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateModuleRequest request)
    {
        var result = await _mediator.Send(request.MapToCommand());
        return Ok(result.MapToResponse());
    }

    // PUT api/<ModulesController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<ModulesController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}

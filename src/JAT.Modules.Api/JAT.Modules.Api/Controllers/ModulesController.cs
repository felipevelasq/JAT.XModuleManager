using JAT.Core.Api;
using JAT.Modules.Api.Contracts.Modules.Create;
using JAT.Modules.Api.Contracts.Modules.List;
using JAT.Modules.Application.Modules.List;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JAT.Modules.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ModulesController(IMediator mediator) : ApiController
{
    private readonly IMediator _mediator = mediator;

    // GET: api/<ModulesController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new ListModulesQuery());
        return result.Match(value => Ok(value.Select(x => x.MapToDTO())), Problem);
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
        return result.Match(value =>
            CreatedAtAction(nameof(Get), new { id = value.Id }, value.MapToResponse()), Problem);
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

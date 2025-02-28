using JAT.Core.Domain.Commons.Results;
using JAT.Modules.Api.Contracts.Modules.Create;
using JAT.Modules.Api.Contracts.Modules.List;
using JAT.Modules.Application.Modules.List;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace JAT.Modules.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ModulesController(IMediator mediator) : ControllerBase
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

    /// <summary>
    /// Base problem method.
    /// </summary>
    /// <param name="errors">A list of <see cref="Error"/>.</param>
    /// <returns>An <see cref="ActionResult"/>.</returns>
    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count is 0)
        {
            return Problem();
        }

        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        return Problem(errors[0]);
    }

    protected IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(
                error.Code,
                error.Message);
        }

        return ValidationProblem(modelStateDictionary);
    }

    protected IActionResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            _ => ErrorTypeToStatusCode(error.Type)
        };

        // HttpContext.Items["AppError"] = error;

        return Problem(statusCode: statusCode, title: error.Message);
    }

    private static int ErrorTypeToStatusCode(ErrorType type)
    {
        var intError = (int)type;

        return intError is >= 100 and <= 599
            ? intError
            : StatusCodes.Status500InternalServerError;
    }
}

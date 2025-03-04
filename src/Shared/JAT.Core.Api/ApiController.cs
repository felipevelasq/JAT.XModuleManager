using JAT.Core.Domain.Commons.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace JAT.Core.Api;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ApiController : ControllerBase
{
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

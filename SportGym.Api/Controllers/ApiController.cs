using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using SportGym.Api.Common.Http;

namespace SportGym.Api.Controllers;

[ApiController]
public class ApiController : ControllerBase
{

  protected IActionResult Problem(List<Error> errors)
  {
    HttpContext.Items[HttpContextItemKeys.Errors] = errors;

    var firstError = errors[0];

    var statusCode = firstError.Type switch
    {
      ErrorType.Conflict => StatusCodes.Status409Conflict,
      ErrorType.Validation => StatusCodes.Status400BadRequest,
      ErrorType.NotFound => StatusCodes.Status404NotFound,
      ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
      ErrorType.Forbidden => StatusCodes.Status403Forbidden,
      _ => StatusCodes.Status500InternalServerError
    };

    return Problem(statusCode: statusCode, title: firstError.Description);
  }

}
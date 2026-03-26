using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using SportGym.Application.Services.Authentication.Commands;
using SportGym.Application.Services.Authentication.Common;
using SportGym.Application.Services.Authentication.Queries;
using SportGym.Contracts.Authentication;
using SportGym.Domain.Common.Errors;

namespace SportGym.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{

  private readonly IAuthenticationCommandService _authenticationCommandService;
  private readonly IAuthenticationQueryService _authenticationQueryService;

  public AuthenticationController(IAuthenticationCommandService authenticationCommandService, IAuthenticationQueryService authenticationQueryService)
  {
    _authenticationCommandService = authenticationCommandService;
    _authenticationQueryService = authenticationQueryService;
  }

  [HttpPost("register")]
  public IActionResult Register(RegisterRequest request)
  {
    ErrorOr<AuthenticationResult> authResult = _authenticationCommandService.Register(
      request.FirstName,
      request.LastName,
      request.Email,
      request.Password
    );

    return authResult.Match(
      authResult => Ok(MapAuthenticationResult(authResult)),
      errors => Problem(errors)
    );

  }

  [HttpPost("login")]
  public IActionResult Login(LoginRequest request)
  {
    var authResult = _authenticationQueryService.Login(
      request.Email,
      request.Password
    );

    if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
    {
      return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);
    }

    return authResult.Match(
      authResult => Ok(MapAuthenticationResult(authResult)),
      errors => Problem(errors)
    );
  }

  private static AuthenticationResponse MapAuthenticationResult(AuthenticationResult authResult)
  {
    return new AuthenticationResponse(
      authResult.User.Id,
      authResult.User.FirstName,
      authResult.User.LastName,
      authResult.User.Email,
      authResult.Token
    );
  }

}
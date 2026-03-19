using Microsoft.AspNetCore.Mvc;
using SportGym.Contracts.Authentication;

namespace SportGym.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
  
  [HttpPost("register")]
  public IActionResult Register(RegisterRequest request)
  {
    return Ok(request);
  }

  [HttpPost("login")]
  public IActionResult Login(LoginRequest request)
  {
    return Ok(request);
  }

}
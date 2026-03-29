using Microsoft.AspNetCore.Mvc;

namespace SportGym.Api.Controllers;

[Route("api/[controller]")]
public class UserController : ApiController
{
    [HttpGet]
    public IActionResult ListUsers()
    {
        return Ok(Array.Empty<string>());
    }
}
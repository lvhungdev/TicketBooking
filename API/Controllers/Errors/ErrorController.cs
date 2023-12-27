using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Errors;

[ApiController]
[Route("api/error")]
public class ErrorController : ControllerBase
{
    [Route("")]
    public IActionResult Error()
    {
        return Problem();
    }
}

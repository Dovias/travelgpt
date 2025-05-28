using Microsoft.AspNetCore.Mvc;

namespace TravelGPT.Server.Controllers.Chat;

[Route("/")]
public class RootController : ControllerBase
{
    [HttpHead]
    public IActionResult GetServerStatus() => Ok();
}
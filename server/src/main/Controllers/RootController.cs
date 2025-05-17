using Microsoft.AspNetCore.Mvc;
using TravelGPT.Server.Dtos.Chat;
using TravelGPT.Server.Services.Chat;

namespace TravelGPT.Server.Controllers.Chat;

[Route("/")]
public class RootController : ControllerBase
{
    [HttpHead]
    public IActionResult GetServerStatus() => Ok();
}
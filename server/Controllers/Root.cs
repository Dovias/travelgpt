using Microsoft.AspNetCore.Mvc;

namespace server.Controllers;

public class Root : Controller {
    [HttpGet("")]
    public IActionResult Get() {
        return Ok("Hello world from travelgpt server!");
    }
}
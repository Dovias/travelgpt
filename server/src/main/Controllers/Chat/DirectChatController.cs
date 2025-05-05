using Microsoft.AspNetCore.Mvc;
using TravelGPT.Server.Dtos.Chat;
using TravelGPT.Server.Services.Chat;

namespace TravelGPT.Server.Controllers.Chat;

[ApiController]
[Route("/chat")]
public class DirectChatController(IDirectChatService service) : ControllerBase
{
    [HttpPost]
    public IActionResult CreateChat()
    => Ok(service.CreateChat());

    [HttpGet("{id}")]
    public IActionResult GetChat(int id)
    => service.TryGetChatResponse(id, out ChatRetrievalResponse response) ? Ok(response) : NotFound();

    [HttpDelete("{id}")]
    public IActionResult DeleteChat(int id) => service.DeleteChat(id) ? Ok() : NotFound();

    [HttpPost("{id}")]
    public IActionResult SendChatMessage(int id, ChatMessageResponseRetrievalRequest request)
    => service.TryGetChatMessageResponse(id, request, out ChatMessageResponseRetrievalResponse response) ? Ok(response) : NotFound();
}
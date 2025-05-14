using Microsoft.AspNetCore.Mvc;
using TravelGPT.Server.Dtos.Chat;
using TravelGPT.Server.Services.Chat;

namespace TravelGPT.Server.Controllers.Chat;

[ApiController]
[Route("/chat")]
public class DirectChatController(IDirectChatService service) : ControllerBase
{
    [HttpPost]
    public IActionResult CreateChat(ChatCreationRequest request)
        => Ok(service.CreateChat(request));

    [HttpGet("{id}")]
    public IActionResult GetChat(Guid id)
        => service.TryGetChatResponse(id, out ChatRetrievalResponse response) ? Ok(response) : NotFound();

    [HttpDelete("{id}")]
    public IActionResult DeleteChat(Guid id) => service.DeleteChat(id) ? Ok() : NotFound();

    [HttpPost("{id}")]
    public IActionResult SendChatMessage(Guid id, ChatMessageRetrievalRequest request)
        => service.TryGetChatMessageResponse(id, request, out ChatMessageRetrievalResponse response) ? Ok(response) : NotFound();
}
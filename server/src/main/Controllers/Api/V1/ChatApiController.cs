using System.Reactive.Linq;
using Microsoft.AspNetCore.Mvc;
using TravelGPT.Server.Dtos.Api.V1;
using TravelGPT.Server.Models.Chat.Direct;

namespace TravelGPT.Server.Controllers.Api.V1;

[ApiController]
[Route("/api/v1/[controller]")]
public class ChatController(IDirectServerChatRepository repository) : ControllerBase
{
    [HttpPost]
    public IActionResult CreateChat()
    {
        IDirectServerChat chat = repository.Create();
        return Ok(new ChatCreationResponse { Id = chat.Id });
    }

    [HttpGet("{id}")]
    public IActionResult GetChat(int id) => repository.TryGet(id, out IDirectServerChat? chat)
        ? Ok(new ChatResponse { Messages = (from message in chat select new ChatMessageResponse { Text = message.Text, Created = message.Created }) })
        : NotFound();

    [HttpDelete("{id}")]
    public IActionResult DeleteChat(int id)
    {
        if (!repository.Contains(id)) return NotFound();

        repository.Delete(id);
        return Ok();
    }

    [HttpPost("{id}")]
    public IActionResult SendChatMessage(int id, SentChatMessageRequest request) =>
        repository.TryGet(id, out IDirectServerChat? chat) ? Ok(new SentChatMessageResponse { Text = chat.Add(request.Text).Received.Text }) : NotFound();
}
using Microsoft.AspNetCore.Mvc;
using TravelGPT.Server.Services;

namespace TravelGPT.Server.Controllers;

[ApiController]
[Route("/chat")]
public class ChatController(ChatService service) : ControllerBase
{
    [HttpPost]
    [Consumes("text/plain")]
    public IActionResult CreateChat([FromBody] string message) =>
        Ok(new
        {
            Id = service.CreateChat(message, out string response),
            Response = response
        });

    [HttpGet("{chatId}")]
    public IActionResult TryFetchAllChatMessages(Guid chatId)
    {
        IEnumerable<string> messages = service.FetchAllChatMessages(chatId);
        if (messages.Any())
        {
            return Ok(messages);
        }
        return NotFound();
    }

    [HttpDelete("{chatId}")]
    public IActionResult TryDeleteChat(Guid chatId) => service.TryDeleteChat(chatId) ? Ok() : NotFound();


    [HttpPost("{chatId}")]
    [Consumes("text/plain")]
    public IActionResult TrySendChatMessage(Guid chatId, [FromBody] string message)
        => service.TrySend(chatId, message, out string? response) ? Ok(response) : NotFound();

    [HttpPut("{chatId}/{chatMessageId}")]
    [Consumes("text/plain")]
    public IActionResult TryEditChatMessage(Guid chatId, int chatMessageId, [FromBody] string message)
        => service.TryEditChatMessage(chatId, chatMessageId, message, out IEnumerable<string>? responses) ? Ok(responses) : NotFound();


    [HttpDelete("{chatId}/{chatMessageId}")]
    public IActionResult TryDeleteChatMessage(Guid chatId, int chatMessageId)
        => service.TryDeleteChatMessage(chatId, chatMessageId) ? Ok() : NotFound();
}
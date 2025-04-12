using Microsoft.AspNetCore.Mvc;
using TravelGPT.Extensions.Chat;
using TravelGPT.Models.Chat;
using TravelGPT.Observers.Chat;

namespace TravelGPT.Controllers.Api.V1.Chat;

[ApiController]
[Route("/api/v1/[controller]")]
public class ChatController(IChatService service, IConfiguration configuration) : ControllerBase
{
    private static readonly int clientUserId = 0;
    private static readonly int serverUserId = 1;

    [HttpPost]
    public IActionResult CreateChat()
    {
        var chat = service.CreateChat();
        var participants = chat.Participants;
        participants.Add(clientUserId);
        chat.Messages.Subscribe(new GeminiChatObserver(new HttpClient(), configuration["GeminiApiKey"]!) { Participant = participants.Add(serverUserId) });

        return Ok(chat);
    }

    [HttpGet("{id}")]
    public IActionResult GetChat(int id)
    {
        IChatContext? context = service.GetChat(id);
        return context == null ? NotFound() : Ok(context);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteChat(int id)
    {
        IChatContext? context = service.GetChat(id);
        if (context == null)
        {
            return NotFound();
        }

        context.Dispose();
        return Ok();
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> SendChatMessage(int id, ChatMessage message)
    {
        IChatContext? chat = service.GetChat(id);
        if (chat == null)
        {
            return NotFound();
        }

        chat.Participants.Get(clientUserId)!.SendMessage(message);
        return Ok(await chat.Messages.Wait());
    }

}
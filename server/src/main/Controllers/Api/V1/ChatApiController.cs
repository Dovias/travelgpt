using System.Reactive.Linq;
using Microsoft.AspNetCore.Mvc;
using TravelGPT.Server.Dtos.Api.V1;
using TravelGPT.Server.Extensions.Chat;
using TravelGPT.Server.Models.Chat;
using TravelGPT.Server.Models.Chat.InMemory;
using TravelGPT.Server.Observers.Chat;

namespace TravelGPT.Server.Controllers.Api.V1;

[ApiController]
[Route("/api/v1/[controller]")]
public class ChatController(IChatRepository chats, IConfiguration config) : ControllerBase
{
    [HttpPost]
    public IActionResult CreateChat()
    {
        IChat chat = chats.Create();
        chat.Subscribe(new GeminiChatObserver(new HttpClient(), config["GeminiApiKey"]!, new InMemoryChatParticipant { Id = 1 }));
        return Ok(new ChatCreationResponse { Id = chat.Id });
    }

    [HttpGet("{id}")]
    public IActionResult GetChat(Guid id) => chats.TryGet(id, out IChat? chat)
        ? Ok(new ChatResponse { Messages = (from message in chat select new ChatMessageResponse { Text = message.Text, Created = message.Created }) })
        : NotFound();

    [HttpDelete("{id}")]
    public IActionResult DeleteChat(Guid id)
    {
        if (!chats.Contains(id))
        {
            return NotFound();
        }
        chats.Delete(id);
        return Ok();
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> SendChatMessage(Guid id, SentChatMessageRequest request)
    {
        if (!chats.TryGet(id, out IChat? chat))
        {
            return NotFound();
        }

        chat!.Add(0, request.Text);
        IChatMessage message = (await chat!.FirstAsync()).Message;
        return Ok(new SentChatMessageResponse { Text = message.Text });
    }
}
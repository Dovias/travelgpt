using Microsoft.AspNetCore.Mvc;
using TravelGPT.Models.Chat;
using TravelGPT.Models.Chat.InMemory;
using TravelGPT.Observers.Chat;
using TravelGPT.Services.Chat;

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
        IChat chat = service.CreateChat();
        chat.AddUser(clientUserId);
        chat.Subscribe(new GeminiChatObserver(new HttpClient(), configuration["GeminiApiKey"]!) { User = chat.AddUser(serverUserId) });

        return Ok(chat);
    }


    [HttpPost("{id}")]
    public async Task<IActionResult> SendChatMessage(Guid id, InMemoryMessage message)
    {
        IChat? chat = service.GetChat(id);
        if (chat == null)
        {
            return NotFound();
        }

        chat.GetUser(clientUserId)!.SendMessage(message.Text);

        TaskCompletionSource<IUserChatMessageContext> source = new();
        IDisposable disposable = chat.Subscribe(new ChatResponseTaskObserver(source));

        IUserChatMessageContext context = await source.Task;
        disposable.Dispose();

        return Ok(context);
    }
}
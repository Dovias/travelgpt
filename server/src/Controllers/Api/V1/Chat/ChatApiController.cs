using Microsoft.AspNetCore.Mvc;
using TravelGPT.Models.Chat;
using TravelGPT.Models.Chat.InMemory;
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
        IChatContext chat = service.CreateChat();
        chat.AddUser(clientUserId);
        chat.Subscribe(new GeminiChatObserver(new HttpClient(), configuration["GeminiApiKey"]!) { User = chat.AddUser(serverUserId) });

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
    public async Task<IActionResult> SendChatMessage(int id, InMemoryChatMessage message)
    {
        IChatContext? chat = service.GetChat(id);
        if (chat == null)
        {
            return NotFound();
        }
        chat.GetUser(clientUserId)!.SendMessage(message.Text);

        return Ok(await WaitForChatMessage(chat));
    }

    private static async Task<IChatMessageContext> WaitForChatMessage(IChatContext chat)
    {
        TaskCompletionSource<IChatMessageContext> source = new();
        IDisposable disposable = chat.Subscribe(new ChatResponseTaskObserver(source));

        IChatMessageContext context = await source.Task;
        disposable.Dispose();

        return context;
    }
}
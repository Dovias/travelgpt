using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using TravelGPT.Services.Chat;
using static TravelGPT.Services.Chat.IChat;

namespace TravelGPT.Controllers.Api.V1;

[ApiController]
[Route("/api/v1/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IDistributedCache _cache;
    private readonly IChatService _service;

    public ChatController(IDistributedCache cache, IChatService service)
    {
        _cache = cache;
        _service = service;


    }

    [HttpPost]
    public IActionResult CreateNewChat()
    {
        return Ok(_service.CreateNewChat());
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> SendChatMessage(Guid id, Message message)
    {
        IChat chat = _service.CreateNewChat();

        var response = new TaskCompletionSource<Message>();
        void callback(MessageContext context)
        {
            response.SetResult(context.Message);
        }
        chat.AddMessage(message);
        chat.Subscribe(callback);

        // I need to wait till callback function returns response
        message = await response.Task;
        chat.Unsubscribe(callback);
        return Ok(message);

    }
}
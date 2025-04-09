using Microsoft.AspNetCore.Mvc;
using TravelGPT.Extensions.Chat;
using TravelGPT.Models.Chat;
using TravelGPT.Services.Chat;

namespace TravelGPT.Controllers.Api.V1.Chat;

[ApiController]
[Route("/api/v1/[controller]")]
public class ChatController(IChatService service) : ControllerBase
{
    [HttpPost]
    public IActionResult CreateNewChat()
    {
        return Ok(service.CreateNewChat());
    }


    [HttpPost("{id}")]
    public async Task<IActionResult> SendChatMessage(Guid id, Message message)
    {
        IChat? chat = service.GetExistingChat(id);
        if (chat == null)
        {
            return NotFound();
        }

        return Ok(await chat.SendAndWaitForMessage(message));
    }
}
using TravelGPT.Models.Chat;

namespace TravelGPT.Services.Chat;

public interface IChatService
{
    IChat CreateChat();
    IChat? GetChat(Guid id);
}

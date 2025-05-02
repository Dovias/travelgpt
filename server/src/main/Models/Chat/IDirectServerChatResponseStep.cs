using TravelGPT.Server.Models.Chat.Direct;

namespace TravelGPT.Server.Models.Chat;

public interface IDirectServerChatResponseStep
{
    bool Step(IDirectServerChat chat, IDirectServerChatMessage sent, ref string response);
}

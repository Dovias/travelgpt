namespace TravelGPT.Server.Models.Chat.Response;

public interface IChatResponseStep
{
    bool Step(IChat chat, IChatMessage sent, ref string response);
}

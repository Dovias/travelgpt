namespace TravelGPT.Server.Models.Chat;

public interface IChatService
{
    IChatContext CreateChat();
    IChatContext? GetChat(int id);
}

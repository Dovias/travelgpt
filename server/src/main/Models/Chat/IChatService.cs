namespace TravelGPT.Models.Chat;

public interface IChatService
{
    IChatContext CreateChat();
    IChatContext? GetChat(int id);
}

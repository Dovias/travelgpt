namespace TravelGPT.Server.Models.Chat.Direct;

public interface IDirectServerChatMessage
{
    int Id { get; }
    string Text { get; }
    DirectServerChatMessageAuthor Author { get; }
    DateTime Created { get; }
}

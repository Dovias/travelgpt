namespace TravelGPT.Models.Chat;

public interface IChatUserContext : IDisposable
{
    int Id { get; }

    IChatMessageContext SendMessage(ChatMessage message);
}

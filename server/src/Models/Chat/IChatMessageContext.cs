namespace TravelGPT.Models.Chat;

public interface IChatMessageContext : IDisposable
{
    int Id { get; }
    ChatMessage Message { get; }
    DateTime Created { get; }
    IChatUserContext User { get; }
}

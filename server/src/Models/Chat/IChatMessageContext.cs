namespace TravelGPT.Models.Chat;

public interface IChatMessageContext : IDisposable
{
    int Id { get; }
    ChatMessage Message { get; }
    DateTime Created { get; }
    IUserChatContext User { get; }
}

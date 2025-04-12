namespace TravelGPT.Models.Chat;

public interface IChatMessageContext : IDisposable
{
    int Id { get; }
    DateTime Created { get; }
    IUserChatContext User { get; }
    ChatMessage Message { get; }
}

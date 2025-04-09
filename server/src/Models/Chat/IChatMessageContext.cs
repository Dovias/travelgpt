namespace TravelGPT.Models.Chat;

public interface IChatMessageContext : IDisposable
{
    int Id { get; }
    DateTime CreatedAt { get; }
    IUserChatContext User { get; }
    IChatMessage Message { get; }
}

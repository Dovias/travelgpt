namespace TravelGPT.Models.Chat;

public interface IChatMessageContext : IDisposable
{
    int Id { get; }
    DateTime Created { get; }
    IUserChatContext User { get; }
    IChatMessage Message { get; }
}

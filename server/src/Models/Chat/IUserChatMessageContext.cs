namespace TravelGPT.Models.Chat;

public interface IUserChatMessageContext : IDisposable
{
    int Id { get; }
    IUserChatMessage Message { get; }
}

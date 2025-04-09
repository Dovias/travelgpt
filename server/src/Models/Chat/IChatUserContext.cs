namespace TravelGPT.Models.Chat;

public interface IUserChatContext : IDisposable
{
    int Id { get; }

    IUserChatMessageContext SendMessage(string text);
}

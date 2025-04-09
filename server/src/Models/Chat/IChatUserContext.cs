namespace TravelGPT.Models.Chat;

public interface IUserChatContext : IDisposable
{
    int Id { get; }

    IChatMessageContext SendMessage(string text);
}

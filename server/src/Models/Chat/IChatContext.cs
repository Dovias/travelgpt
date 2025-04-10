namespace TravelGPT.Models.Chat;

public interface IChatContext : IObservable<IChatMessageContext>, IDisposable
{
    public int Id { get; }

    IEnumerable<IUserChatContext> Users { get; }
    IEnumerable<IChatMessageContext> Messages { get; }

    IUserChatContext AddUser(int id);
    IUserChatContext? GetUser(int id);
}

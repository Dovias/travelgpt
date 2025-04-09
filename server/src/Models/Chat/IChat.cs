namespace TravelGPT.Models.Chat;

public interface IChat : IObservable<IChatMessageContext>
{
    IEnumerable<IUserChatContext> Users { get; }
    IEnumerable<IChatMessageContext> Messages { get; }

    IUserChatContext AddUser(int id);
    IUserChatContext? GetUser(int id);
}
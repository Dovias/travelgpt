namespace TravelGPT.Models.Chat;

public interface IChat : IObservable<IUserChatMessageContext>
{
    IEnumerable<IUserChatContext> Users { get; }
    IEnumerable<IUserChatMessageContext> Messages { get; }

    IUserChatContext AddUser(int id);
    IUserChatContext? GetUser(int id);
}
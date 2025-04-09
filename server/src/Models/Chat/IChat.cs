namespace TravelGPT.Models.Chat;

public interface IChat : IObservable<IUserChatMessageContext>
{
    Guid Id { get; }

    IUserChatContext AddUser(int id);
    IUserChatContext? GetUser(int id);
}
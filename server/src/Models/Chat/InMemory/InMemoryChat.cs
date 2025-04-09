namespace TravelGPT.Models.Chat.InMemory;

public class InMemoryChat(IDictionary<int, IUserChatContext> users, IDictionary<int, IUserChatMessageContext> messages, ISet<IObserver<IUserChatMessageContext>> observers) : IChat
{
    public IEnumerable<IUserChatContext> Users => users.Values;
    public IEnumerable<IUserChatMessageContext> Messages => messages.Values;

    public IUserChatContext AddUser(int id)
    {
        InMemoryChatUserContext context = new(new WeakReference<IDictionary<int, IUserChatContext>>(users), messages, observers)
        {
            Id = id
        };

        users.Add(id, context);

        return context;
    }

    public IUserChatContext? GetUser(int id) => users[id];

    private class RegisteredChatObserver(ISet<IObserver<IUserChatMessageContext>> observers, IObserver<IUserChatMessageContext> observer) : IDisposable
    {
        public void Dispose() => observers.Remove(observer);
    }

    public IDisposable Subscribe(IObserver<IUserChatMessageContext> observer)
    {
        if (!observers.Add(observer))
        {
            throw new ArgumentException("Provided message context observer is already subscribed to the chat");
        }

        return new RegisteredChatObserver(observers, observer);
    }
}

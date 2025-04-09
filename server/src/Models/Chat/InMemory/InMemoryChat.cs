namespace TravelGPT.Models.Chat.InMemory;

public class InMemoryChat(IDictionary<int, IUserChatContext> users, IDictionary<int, IChatMessageContext> messages, ISet<IObserver<IChatMessageContext>> observers) : IChat
{
    public IEnumerable<IUserChatContext> Users => users.Values;
    public IEnumerable<IChatMessageContext> Messages => messages.Values;

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

    private class RegisteredChatObserver(ISet<IObserver<IChatMessageContext>> observers, IObserver<IChatMessageContext> observer) : IDisposable
    {
        public void Dispose() => observers.Remove(observer);
    }

    public IDisposable Subscribe(IObserver<IChatMessageContext> observer)
    {
        if (!observers.Add(observer))
        {
            throw new ArgumentException("Provided message context observer is already subscribed to the chat");
        }

        return new RegisteredChatObserver(observers, observer);
    }
}

namespace TravelGPT.Models.Chat.InMemory;


public class InMemoryChatContext(WeakReference<IDictionary<int, IChatContext>> chats, IDictionary<int, IChatUserContext> users, IDictionary<int, IChatMessageContext> messages, ISet<IObserver<IChatMessageContext>> observers) : IChatContext
{
    private IDictionary<int, IChatContext>? Chats
    {
        get => chats.TryGetTarget(out IDictionary<int, IChatContext>? contexts) ? contexts : null;
    }

    public required int Id { get; init; }

    public IEnumerable<IChatUserContext> Users => users.Values;
    public IEnumerable<IChatMessageContext> Messages => messages.Values;

    public IChatUserContext AddUser(int id)
    {
        InMemoryChatUserContext context = new(new WeakReference<IDictionary<int, IChatUserContext>>(users), messages, observers)
        {
            Id = id
        };

        users.Add(id, context);

        return context;
    }

    public IChatUserContext? GetUser(int id) => users[id];

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

    public void Dispose() => Chats?.Remove(Id);
}

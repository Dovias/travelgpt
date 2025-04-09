namespace TravelGPT.Models.Chat.InMemory;

public class InMemoryChat(IDictionary<int, IUserChatContext> users, IDictionary<int, IUserChatMessageContext> messages, ISet<IObserver<IUserChatMessageContext>> observers) : IChat
{
    public required Guid Id { get; init; }

    public IUserChatContext AddUser(int id)
    {
        InMemoryChatUserContext context = new(new WeakReference<IDictionary<int, IUserChatContext>>(users), messages, observers)
        {
            Id = id
        };

        users.Add(id, context);

        return context;
    }

    public IUserChatContext? GetUser(int id)
    {
        return users[id];
    }

    private class RegisteredChatObserver(ISet<IObserver<IUserChatMessageContext>> observers, IObserver<IUserChatMessageContext> observer) : IDisposable
    {
        public void Dispose() => observers.Remove(observer);
    }

    public IDisposable Subscribe(IObserver<IUserChatMessageContext> observer)
    {
        if (!observers.Add(observer))
        {
            throw new ArgumentException("Provided message context observer is already subscribed to the chat with id: " + Id);
        }

        return new RegisteredChatObserver(observers, observer);
    }
}

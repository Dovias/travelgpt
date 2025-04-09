namespace TravelGPT.Models.Chat.InMemory;

public class InMemoryChatUserContext(WeakReference<IDictionary<int, IUserChatContext>> users, IDictionary<int, IUserChatMessageContext> messages, ISet<IObserver<IUserChatMessageContext>> observers) : IUserChatContext
{
    private static int Counter;

    public required int Id { get; init; }

    private IDictionary<int, IUserChatContext>? Users
    {
        get => users.TryGetTarget(out IDictionary<int, IUserChatContext>? contexts) ? contexts : null;
    }

    public IUserChatMessageContext SendMessage(string text)
    {
        if (!Users?.ContainsKey(Id) ?? false)
        {
            throw new ObjectDisposedException("Attempt to send chat message with already disposed user context");
        }

        InMemoryChatUserMessageContext context = new(new WeakReference<IDictionary<int, IUserChatMessageContext>>(messages))
        {
            Id = Counter++,
            Message = new InMemoryUserChatMessage()
            {
                User = this,
                Text = text,
                CreatedAt = DateTime.Now
            }
        };

        messages.Add(context.Id, context);
        NotifyObservers(context);
        return context;
    }

    private void NotifyObservers(IUserChatMessageContext context)
    {
        foreach (var observer in observers)
        {
            observer.OnNext(context);
        }
    }

    public void Dispose()
    {
        Users?.Remove(Id);
    }

}


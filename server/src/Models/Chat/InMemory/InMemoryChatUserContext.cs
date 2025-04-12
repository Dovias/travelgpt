namespace TravelGPT.Models.Chat.InMemory;

public class InMemoryChatUserContext(WeakReference<IDictionary<int, IChatUserContext>> users, IDictionary<int, IChatMessageContext> messages, ISet<IObserver<IChatMessageContext>> observers) : IChatUserContext
{
    public required int Id { get; init; }

    private IDictionary<int, IChatUserContext>? Users
    {
        get => users.TryGetTarget(out IDictionary<int, IChatUserContext>? contexts) ? contexts : null;
    }
    private bool Disposed { get => !Users?.ContainsKey(Id) ?? false; }
    private static int Counter;

    public IChatMessageContext SendMessage(ChatMessage message)
    {
        if (Disposed)
        {
            throw new ObjectDisposedException("Attempt to send chat message with already disposed user context");
        }

        InMemoryChatMessageContext context = new(new WeakReference<IDictionary<int, IChatMessageContext>>(messages))
        {
            Id = Counter++,
            Created = DateTime.Now,
            User = this,
            Message = message
        };

        messages.Add(context.Id, context);
        NotifyObservers(context);
        return context;
    }

    private void NotifyObservers(IChatMessageContext context)
    {
        foreach (var observer in observers)
        {
            observer.OnNext(context);
        }
    }

    public void Dispose() => Users?.Remove(Id);

}


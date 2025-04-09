namespace TravelGPT.Models.Chat.InMemory;

public class InMemoryChatUserContext(WeakReference<IDictionary<int, IUserChatContext>> users, IDictionary<int, IChatMessageContext> messages, ISet<IObserver<IChatMessageContext>> observers) : IUserChatContext
{
    private static int Counter;

    public required int Id { get; init; }

    private IDictionary<int, IUserChatContext>? Users
    {
        get => users.TryGetTarget(out IDictionary<int, IUserChatContext>? contexts) ? contexts : null;
    }

    public IChatMessageContext SendMessage(string text)
    {
        if (!Users?.ContainsKey(Id) ?? false)
        {
            throw new ObjectDisposedException("Attempt to send chat message with already disposed user context");
        }

        InMemoryChatMessageContext context = new(new WeakReference<IDictionary<int, IChatMessageContext>>(messages))
        {
            Id = Counter++,
            Created = DateTime.Now,
            User = this,
            Message = new InMemoryChatMessage()
            {
                Text = text,
            }
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

    public void Dispose()
    {
        Users?.Remove(Id);
    }

}


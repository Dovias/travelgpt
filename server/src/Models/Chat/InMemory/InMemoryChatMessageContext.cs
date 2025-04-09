namespace TravelGPT.Models.Chat.InMemory;

public class InMemoryChatMessageContext(WeakReference<IDictionary<int, IChatMessageContext>> contexts) : IChatMessageContext
{
    private IDictionary<int, IChatMessageContext>? Messages
    {
        get
        {
            IDictionary<int, IChatMessageContext>? messages;
            return contexts.TryGetTarget(out messages) ? messages : null;
        }
    }

    public required int Id { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required IUserChatContext User { get; init; }
    public required IChatMessage Message { get; init; }

    public void Dispose()
    {
        Messages?.Remove(Id);
    }
}


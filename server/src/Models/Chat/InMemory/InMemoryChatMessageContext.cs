namespace TravelGPT.Models.Chat.InMemory;

public class InMemoryChatMessageContext(WeakReference<IDictionary<int, IChatMessageContext>> contexts) : IChatMessageContext
{
    private IDictionary<int, IChatMessageContext>? Messages
    {
        get => contexts.TryGetTarget(out IDictionary<int, IChatMessageContext>? messages) ? messages : null;
    }

    public required int Id { get; init; }
    public required DateTime Created { get; init; }
    public required IUserChatContext User { get; init; }
    public required ChatMessage Message { get; init; }

    public void Dispose() => Messages?.Remove(Id);
}


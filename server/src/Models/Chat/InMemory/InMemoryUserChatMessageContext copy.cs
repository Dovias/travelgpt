namespace TravelGPT.Models.Chat.InMemory;

public class InMemoryChatUserMessageContext(WeakReference<IDictionary<int, IUserChatMessageContext>> contexts) : IUserChatMessageContext
{
    private IDictionary<int, IUserChatMessageContext>? Contexts
    {
        get
        {
            IDictionary<int, IUserChatMessageContext>? messages;
            return contexts.TryGetTarget(out messages) ? messages : null;
        }
    }

    public required int Id { get; init; }
    public required IUserChatMessage Message { get; init; }

    public void Dispose()
    {
        Contexts?.Remove(Id);
    }
}


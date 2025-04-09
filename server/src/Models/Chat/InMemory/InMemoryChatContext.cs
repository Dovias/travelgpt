namespace TravelGPT.Models.Chat.InMemory;

public class InMemoryChatContext(WeakReference<IDictionary<int, IChatContext>> chats) : IChatContext
{
    private IDictionary<int, IChatContext>? Chats
    {
        get => chats.TryGetTarget(out IDictionary<int, IChatContext>? contexts) ? contexts : null;
    }

    public required int Id { get; init; }
    public required IChat Chat { get; init; }

    public void Dispose()
    {
        Chats?.Remove(Id);
    }
}

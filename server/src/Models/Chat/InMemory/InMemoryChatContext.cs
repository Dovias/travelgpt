namespace TravelGPT.Models.Chat.InMemory;


public class InMemoryChatContext(WeakReference<IDictionary<int, IChatContext>> reference) : IChatContext
{
    public required int Id { get; init; }
    public required IChatParticipantContextCollection Participants { get; init; }
    public required IChatMessageContextCollection Messages { get; init; }

    private IDictionary<int, IChatContext>? Chats
    {
        get => reference.TryGetTarget(out IDictionary<int, IChatContext>? contexts) ? contexts : null;
    }

    public void Dispose() => Chats?.Remove(Id);
}

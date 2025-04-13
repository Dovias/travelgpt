using System.Collections.ObjectModel;

namespace TravelGPT.Server.Models.Chat.InMemory;

public class InMemoryChatService(IDictionary<int, IChatContext> chats) : IChatService
{
    private static int Counter;

    public IChatContext CreateChat()
    {
        Dictionary<int, IChatMessageContext> contexts = [];
        Collection<IObserver<IChatMessageContext>> observers = [];

        InMemoryChatContext context = new(new WeakReference<IDictionary<int, IChatContext>>(chats))
        {
            Id = Counter++,
            Participants = new InMemoryChatParticipantContextCollection(new Dictionary<int, IChatParticipantContext>(), [], contexts, observers),
            Messages = new InMemoryChatMessageContextCollection(contexts, observers)
        };

        chats.Add(context.Id, context);
        return context;
    }

    public IChatContext? GetChat(int id) => chats.TryGetValue(id, out IChatContext? context) ? context : null;
}
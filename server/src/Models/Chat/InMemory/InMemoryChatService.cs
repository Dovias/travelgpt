namespace TravelGPT.Models.Chat.InMemory;

public class InMemoryChatService(IDictionary<int, IChatContext> chats, int counter) : IChatService
{
    public IChatContext CreateChat()
    {
        InMemoryChatContext context = new(new WeakReference<IDictionary<int, IChatContext>>(chats), new Dictionary<int, IUserChatContext>(),
                new Dictionary<int, IChatMessageContext>(),
                new HashSet<IObserver<IChatMessageContext>>())
        {
            Id = counter++
        };

        chats.Add(context.Id, context);
        return context;
    }

    public IChatContext? GetChat(int id) => chats.TryGetValue(id, out IChatContext? context) ? context : null;
}
using System.Collections;

namespace TravelGPT.Server.Models.Chat.InMemory;

public class InMemoryChatRepository(IDictionary<Guid, ChatContext> chats, IDictionary<int, ChatMessageContext> messages) : IChatRepository
{
    public ChatContext Create()
    {
        Guid id = Guid.NewGuid();
        ChatContext context = new()
        {
            Id = id,
            Messages = new InMemoryChat(messages)
        };

        chats.Add(id, context);
        return context;
    }

    public bool Delete(Guid id) => chats.Remove(id);

    public bool TryGet(Guid id, out ChatContext chat) => chats.TryGetValue(id, out chat);

    public bool Contains(Guid id) => chats.ContainsKey(id);

    public IEnumerator<ChatContext> GetEnumerator() => chats.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

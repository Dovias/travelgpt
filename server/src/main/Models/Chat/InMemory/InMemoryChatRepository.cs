using System.Collections;

namespace TravelGPT.Server.Models.Chat.InMemory;

public class InMemoryChatRepository(IDictionary<int, ChatContext> chats, IDictionary<int, ChatMessageContext> messages) : IChatRepository
{
    private int _counter;

    public ChatContext Create()
    {
        int id = _counter++;
        ChatContext context = new()
        {
            Id = id,
            Messages = new InMemoryChat(messages)
        };

        chats.Add(id, context);
        return context;
    }

    public bool Delete(int id) => chats.Remove(id);

    public bool TryGet(int id, out ChatContext chat) => chats.TryGetValue(id, out chat);

    public bool Contains(int id) => chats.ContainsKey(id);

    public IEnumerator<ChatContext> GetEnumerator() => chats.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

using System.Collections;

namespace TravelGPT.Server.Models.Chat.Dictionary;

public class DictionaryChatRepository(IDictionary<Guid, ChatContext> chats) : IChatRepository
{
    public ChatContext Create()
    {
        Guid id = Guid.NewGuid();
        ChatContext context = new()
        {
            Id = id,
            Messages = new DictionaryChat(new Dictionary<int, ChatMessageContext>())
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

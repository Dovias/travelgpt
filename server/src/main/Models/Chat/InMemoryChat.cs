using System.Collections;
using TravelGPT.Server.Models.User;

namespace TravelGPT.Server.Models.Chat;

public class InMemoryChat(IDictionary<int, ChatMessageContext> contexts) : IChat
{
    private static int _counter;

    public ChatMessageContext Add(UserContext author, ChatMessage message)
    {
        int id = _counter++;

        ChatMessageContext context = new()
        {
            Id = id,
            Message = message,
            Author = author,
            Created = DateTime.Now
        };

        contexts.Add(id, context);
        return context;
    }

    public bool Remove(int id) => contexts.Remove(id);

    public bool TryGet(int id, out ChatMessageContext message) => contexts.TryGetValue(id, out message);

    public bool Contains(int id) => contexts.ContainsKey(id);

    public ChatMessageContext this[int id] => contexts[id];

    public IEnumerator<ChatMessageContext> GetEnumerator() => contexts.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

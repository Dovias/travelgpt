using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace TravelGPT.Server.Models.Chat.InMemory;

public class InMemoryChatRepository(IDictionary<int, IChat> chats, IDictionary<int, IChatMessage> messages) : IChatRepository
{
    private int _counter;

    public IChat Create()
    {
        int id = _counter++;
        IChat chat = new InMemoryChat(messages) { Id = id };

        chats.Add(id, chat);
        return chat;
    }

    public bool Delete(int id) => chats.Remove(id);

    public bool TryGet(int id, [NotNullWhen(true)] out IChat? chat) => chats.TryGetValue(id, out chat);

    public bool Contains(int id) => chats.ContainsKey(id);

    public IEnumerator<IChat> GetEnumerator() => chats.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

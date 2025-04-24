using System.Collections;

namespace TravelGPT.Server.Models.Chat.InMemory;

public class InMemoryChatRepository(IDictionary<int, IChat> chats, Func<int, IChat> factory) : IChatRepository
{
    private static int _counter;

    public IChat Create()
    {
        int id = _counter++;
        IChat chat = factory.Invoke(id);

        chats.Add(id, chat);
        return chat;
    }

    public bool Delete(int id) => chats.Remove(id);

    public bool TryGet(int id, out IChat? chat) => chats.TryGetValue(id, out chat);

    public bool Contains(int id) => chats.ContainsKey(id);

    public IEnumerator<IChat> GetEnumerator() => chats.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

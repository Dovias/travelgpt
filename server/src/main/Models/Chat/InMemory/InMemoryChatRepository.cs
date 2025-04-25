using System.Collections;

namespace TravelGPT.Server.Models.Chat.InMemory;

public class InMemoryChatRepository(IDictionary<Guid, IChat> chats, Func<Guid, IChat> factory) : IChatRepository
{
    public IChat Create()
    {
        Guid id = Guid.NewGuid();
        IChat chat = factory.Invoke(id);

        chats.Add(id, chat);
        return chat;
    }

    public bool Delete(Guid id) => chats.Remove(id);

    public bool TryGet(Guid id, out IChat? chat) => chats.TryGetValue(id, out chat);

    public bool Contains(Guid id) => chats.ContainsKey(id);

    public IEnumerator<IChat> GetEnumerator() => chats.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

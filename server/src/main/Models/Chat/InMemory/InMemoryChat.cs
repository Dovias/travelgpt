using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace TravelGPT.Server.Models.Chat.InMemory;

public class InMemoryChat(IDictionary<int, IChatMessage> messages) : IChat
{
    public required int Id { get; init; }

    private static int _counter;

    public IChatMessage Add(int authorId, string text)
    {
        int id = _counter++;

        InMemoryChatMessage message = new()
        {
            Id = id,
            AuthorId = authorId,
            Text = text,
            Created = DateTime.Now
        };

        messages.Add(id, message);
        return message;
    }

    public bool Remove(int id) => messages.Remove(id);

    public bool TryGet(int id, [NotNullWhen(true)] out IChatMessage? message) => messages.TryGetValue(id, out message);

    public bool Contains(int id) => messages.ContainsKey(id);

    public IChatMessage this[int id] => messages[id];

    public IEnumerator<IChatMessage> GetEnumerator() => messages.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

using System.Collections;

namespace TravelGPT.Server.Models.Chat.InMemory;

public class InMemoryChat(IDictionary<int, IChatMessage> messages, ICollection<IObserver<IChatMessageContext>> observers, Func<int, IChatMessageDetails, IChatMessage> messageFactory, Func<IChat, IChatMessage, IChatMessageContext> contextFactory) : IChat
{
    public required int Id { get; init; }

    private static int _counter;

    public IChatMessage Add(IChatMessageDetails details)
    {
        int id = _counter++;
        IChatMessage message = messageFactory.Invoke(id, details);

        messages.Add(id, message);

        IChatMessageContext context = contextFactory.Invoke(this, message);
        foreach (var observer in observers.ToArray())
        {
            observer.OnNext(context);
        }

        return message;
    }

    public bool Remove(int id) => messages.Remove(id);

    public bool TryGet(int id, out IChatMessage? message) => messages.TryGetValue(id, out message);

    public bool Contains(int id) => messages.ContainsKey(id);

    public IChatMessage this[int id] => messages[id];

    public IDisposable Subscribe(IObserver<IChatMessageContext> observer)
    {
        observers.Add(observer);
        return new InMemoryChatObserverDisposable(observers, observer);
    }

    public IEnumerator<IChatMessage> GetEnumerator() => messages.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

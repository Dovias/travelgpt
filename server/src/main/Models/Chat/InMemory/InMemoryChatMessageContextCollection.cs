using System.Collections;

namespace TravelGPT.Models.Chat.InMemory;

public class InMemoryChatMessageContextCollection(IDictionary<int, IChatMessageContext> messages, ICollection<IObserver<IChatMessageContext>> observers) : IChatMessageContextCollection
{
    public IChatMessageContext? Get(int id)
    {
        return messages.TryGetValue(id, out IChatMessageContext? context) ? context : null;
    }

    public IEnumerator<IChatMessageContext> GetEnumerator()
    {
        return (from message in messages.Values select message).GetEnumerator();
    }

    public IDisposable Subscribe(IObserver<IChatMessageContext> observer)
    {
        observers.Add(observer);
        return new InMemoryChatMessageContextObserverDisposable(observers, observer);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}

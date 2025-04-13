using System.Collections;

namespace TravelGPT.Server.Models.Chat.InMemory;

public class InMemoryChatParticipantContextCollection(IDictionary<int, IChatParticipantContext> users, ICollection<IObserver<IChatParticipantContext>> userObservers, IDictionary<int, IChatMessageContext> messages, ICollection<IObserver<IChatMessageContext>> messageObservers) : IChatParticipantContextCollection
{
    public IChatParticipantContext Add(int id)
    {
        // TODO: implement implicit ids
        // int id = userCounter++;
        InMemoryChatParticipantContext context = new(new WeakReference<IDictionary<int, IChatParticipantContext>>(users), messages, messageObservers)
        {
            Id = id
        };

        users.Add(id, context);
        return context;
    }

    public IChatParticipantContext? Get(int id)
    {
        return users.TryGetValue(id, out IChatParticipantContext? context) ? context : null;
    }

    public IEnumerator<IChatParticipantContext> GetEnumerator()
    {
        return (from user in users.Values select user).GetEnumerator();
    }

    public IDisposable Subscribe(IObserver<IChatParticipantContext> observer)
    {
        userObservers.Add(observer);
        return new InMemoryUserChatContextObserverDisposable(userObservers, observer);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}

namespace TravelGPT.Server.Models.Chat;

public interface IChat : IEnumerable<IChatMessage>, IObservable<IChatMessageContext>
{
    int Id { get; }

    IChatMessage Add(IChatMessageDetails details);
    bool Remove(int id);

    bool TryGet(int id, out IChatMessage? message);
    bool Contains(int id);

    public IChatMessage this[int id];
    }

namespace TravelGPT.Server.Models.Chat.InMemory;

public class InMemoryUserChatContextObserverDisposable(ICollection<IObserver<IChatParticipantContext>> observers, IObserver<IChatParticipantContext> observer) : IDisposable
{
    public void Dispose() => observers.Remove(observer);

}


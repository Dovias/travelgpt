namespace TravelGPT.Server.Models.Chat.InMemory;

public class InMemoryChatMessageContextObserverDisposable(ICollection<IObserver<IChatMessageContext>> observers, IObserver<IChatMessageContext> observer) : IDisposable
{
    public void Dispose() => observers.Remove(observer);

}


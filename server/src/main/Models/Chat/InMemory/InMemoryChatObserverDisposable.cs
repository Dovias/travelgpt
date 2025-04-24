namespace TravelGPT.Server.Models.Chat.InMemory;

public class InMemoryChatObserverDisposable(ICollection<IObserver<IChatMessageContext>> observers, IObserver<IChatMessageContext> observer) : IDisposable
{
    private bool _disposed;

    public void Dispose()
    {
        if (!_disposed)
        {
            observers.Remove(observer);
            _disposed = true;
        }
    }

}

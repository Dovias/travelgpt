namespace TravelGPT.Models.Chat;

public class InMemoryChat(ISet<IObserver<MessageContext>> observers, IList<Message> messages) : IChat
{
    private readonly ISet<IObserver<MessageContext>> _observers = observers;
    private readonly IList<Message> _messages = messages;

    public required Guid Id { get; init; }

    public void AddMessage(Message message)
    {
        _messages.Add(message);
        foreach (var callback in _observers)
        {
            callback.OnNext(new(this, message));
        }
    }

    public Message GetMessage(int id)
    {
        return _messages[id];
    }

    private class RegisteredChatObserver(ISet<IObserver<MessageContext>> observers, IObserver<MessageContext> observer) : IDisposable
    {
        private readonly ISet<IObserver<MessageContext>> callbacks = observers;
        private readonly IObserver<MessageContext> callback = observer;

        public void Dispose() => callbacks.Remove(callback);
    }

    public IDisposable Subscribe(IObserver<MessageContext> observer)
    {
        if (!_observers.Add(observer))
        {
            throw new ArgumentException("Provided message context observer is already subscribed to the chat with id: " + Id);
        }

        return new RegisteredChatObserver(_observers, observer);
    }
}

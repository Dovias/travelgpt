using static TravelGPT.Services.Chat.IChat;

namespace TravelGPT.Services.Chat;

public class InMemoryChat : IChat
{
    private ISet<Action<MessageContext>> _callbacks;
    // Maybe linked list is better in this case?
    private List<Message> _messages = new();

    public required Guid Id { get; init; }

    public InMemoryChat(ISet<Action<MessageContext>> callbacks)
    {
        _callbacks = callbacks;
    }

    public void AddMessage(Message message)
    {
        _messages.Add(message);
        foreach (var callback in _callbacks)
        {
            callback(new(this, message));
        }
    }

    public Message GetMessage(int id)
    {
        return _messages[id];
    }

    public ICollection<Message> GetAllMessages()
    {
        return _messages;
    }

    public bool Subscribe(Action<MessageContext> callback)
    {
        return _callbacks.Add(callback);
    }

    public bool Unsubscribe(Action<MessageContext> callback)
    {
        return _callbacks.Remove(callback);
    }
}

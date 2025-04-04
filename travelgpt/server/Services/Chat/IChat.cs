namespace TravelGPT.Services.Chat;

public interface IChat
{
    Guid Id { get; }

    void AddMessage(Message message);
    Message GetMessage(int id);
    ICollection<Message> GetAllMessages();

    bool Subscribe(Action<MessageContext> callback);
    bool Unsubscribe(Action<MessageContext> callback);

    public record Message(Guid Author, string Text);
    public record MessageContext(IChat Chat, Message Message);
}
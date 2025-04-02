namespace TravelGPT.Models.Chat;

public record Message(Guid Author, string Text);
public record MessageContext(IChat Chat, Message Message);

public interface IChat : IObservable<MessageContext>
{
    Guid Id { get; }

    void AddMessage(Message message);
    Message GetMessage(int id);
}
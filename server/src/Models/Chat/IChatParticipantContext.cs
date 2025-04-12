namespace TravelGPT.Models.Chat;

public interface IChatParticipantContext : IDisposable
{
    int Id { get; }

    IChatMessageContext SendMessage(ChatMessage message);
}

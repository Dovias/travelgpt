namespace TravelGPT.Server.Models.Chat;

public interface IChatMessageContext : IDisposable
{
    int Id { get; }

    ChatMessage Message { get; }
    DateTime Created { get; }
    IChatParticipantContext Participant { get; }
}

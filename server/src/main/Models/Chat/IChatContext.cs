namespace TravelGPT.Server.Models.Chat;

public interface IChatContext : IDisposable
{
    public int Id { get; }

    IChatParticipantContextCollection Participants { get; }
    IChatMessageContextCollection Messages { get; }
}

namespace TravelGPT.Server.Models.Chat.InMemory;

public record InMemoryChatParticipant : IChatParticipant
{
    public required int Id { get; init; }
}


namespace TravelGPT.Server.Models.Chat.InMemory;

public readonly record struct InMemoryChatMessage() : IChatMessage
{
    public required int Id { get; init; }
    public required IChatParticipant Author { get; init; }

    public required string Text { get; init; }
    public required DateTime Created { get; init; }

}

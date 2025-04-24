namespace TravelGPT.Server.Models.Chat.InMemory;

public readonly record struct InMemoryChatMessageDetails() : IChatMessageDetails
{
    public required IChatParticipant Author { get; init; }

    public required string Text { get; init; }
}

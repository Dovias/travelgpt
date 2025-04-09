namespace TravelGPT.Models.Chat.InMemory;

public record InMemoryChatMessage : IChatMessage
{
    public required string Text { get; init; }
}
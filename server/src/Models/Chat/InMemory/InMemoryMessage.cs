namespace TravelGPT.Models.Chat.InMemory;

public record InMemoryMessage : IMessage
{
    public required string Text { get; init; }
}
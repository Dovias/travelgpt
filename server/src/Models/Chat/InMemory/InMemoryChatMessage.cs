
namespace TravelGPT.Models.Chat.InMemory;

public record InMemoryChatMessage : InMemoryMessage, IChatMessage
{
    public required DateTime CreatedAt { get; init; }
}
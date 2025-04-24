
namespace TravelGPT.Server.Models.Chat.InMemory;

public readonly record struct InMemoryMessageDetails
{
    public required string Text { get; init; }
}

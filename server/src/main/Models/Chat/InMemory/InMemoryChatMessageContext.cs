
namespace TravelGPT.Server.Models.Chat.InMemory;

public readonly record struct InMemoryChatMessageContext() : IChatMessageContext
{
    public required IChat Chat { get; init; }
    public required IChatMessage Message { get; init; }
}

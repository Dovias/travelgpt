using TravelGPT.Server.Models.Chat.Direct;

namespace TravelGPT.Server.Models.Chat.InMemory;

public readonly record struct InMemoryDirectServerChatMessageResponse : IDirectServerChatMessageResponse
{
    public required IDirectServerChatMessage Sent { get; init; }
    public required IDirectServerChatMessage Received { get; init; }
}

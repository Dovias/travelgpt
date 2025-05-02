using TravelGPT.Server.Models.Chat.Direct;

namespace TravelGPT.Server.Models.Chat.InMemory;

public readonly record struct InMemoryDirectServerChatMessage : IDirectServerChatMessage
{
    public required int Id { get; init; }
    public required DirectServerChatMessageAuthor Author { get; init; }

    public required string Text { get; init; }
    public required DateTime Created { get; init; }
}

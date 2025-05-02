using TravelGPT.Server.Models.Chat.Direct;

namespace TravelGPT.Server.Models.Chat.Llm;

public readonly record struct InMemoryLlmMessage : ILlmMessage
{
    public required string Text { get; init; }
    public required DirectServerChatMessageAuthor Author { get; init; }
}

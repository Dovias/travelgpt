namespace TravelGPT.Server.Models.Chat.Llm;

public readonly record struct InMemoryLlmResponse : ILlmResponse
{
    public required string Text { get; init; }
}

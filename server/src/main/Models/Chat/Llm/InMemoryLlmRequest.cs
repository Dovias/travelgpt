namespace TravelGPT.Server.Models.Chat.Llm;

public readonly record struct InMemoryLlmRequest : ILlmRequest
{
    public required IEnumerable<string> Instructions { get; init; }
    public required IEnumerable<ILlmMessage> Messages { get; init; }
}

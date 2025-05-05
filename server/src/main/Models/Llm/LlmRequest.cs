namespace TravelGPT.Server.Models.Llm;

public readonly record struct LlmRequest
{
    public required IEnumerable<string> Instructions { get; init; }
    public required IEnumerable<LlmMessage> Messages { get; init; }
}

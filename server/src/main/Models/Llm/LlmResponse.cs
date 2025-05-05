namespace TravelGPT.Server.Models.Llm;

public readonly record struct LlmResponse
{
    public required IEnumerable<string> Text { get; init; }
}

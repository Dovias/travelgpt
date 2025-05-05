namespace TravelGPT.Server.Models.Llm;

public readonly record struct LlmResponse
{
    public required string Text { get; init; }
}

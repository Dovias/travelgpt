namespace TravelGPT.Server.Models.Llm;

public readonly record struct LlmMessage
{
    public required string Text { get; init; }
    public required LlmMessageRole Role { get; init; }
}

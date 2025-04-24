namespace TravelGPT.Server.Dtos.Api.V1;

public readonly record struct ChatMessageResponse
{
    public required string Text { get; init; }
    public required DateTime Created { get; init; }
}
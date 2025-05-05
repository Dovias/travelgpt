namespace TravelGPT.Server.Dtos.Api.V1.Chat;

public readonly record struct ChatMessageResponseRetrievalRequest
{
    public required string Text { get; init; }
}

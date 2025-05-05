namespace TravelGPT.Server.Dtos.Chat;

public readonly record struct ChatMessageResponseRetrievalRequest
{
    public required string Text { get; init; }
}

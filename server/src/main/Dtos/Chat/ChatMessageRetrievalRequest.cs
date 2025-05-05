namespace TravelGPT.Server.Dtos.Chat;

public readonly record struct ChatMessageRetrievalRequest
{
    public required string Text { get; init; }
}

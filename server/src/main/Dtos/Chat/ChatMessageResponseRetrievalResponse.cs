namespace TravelGPT.Server.Dtos.Chat;

public readonly record struct ChatMessageResponseRetrievalResponse
{
    public required string Text { get; init; }
}

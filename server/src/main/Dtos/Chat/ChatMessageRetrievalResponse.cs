namespace TravelGPT.Server.Dtos.Chat;

public readonly record struct ChatMessageRetrievalResponse
{
    public required string Text { get; init; }
}

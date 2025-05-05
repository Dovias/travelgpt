namespace TravelGPT.Server.Dtos.Api.V1.Chat;

public readonly record struct ChatMessageRetrievalResponse
{
    public required string Text { get; init; }
}

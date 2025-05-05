namespace TravelGPT.Server.Dtos.Chat;

public readonly record struct ChatRetrievalResponse
{
    public required IEnumerable<ChatMessageResponseRetrievalResponse> Messages { get; init; }
}

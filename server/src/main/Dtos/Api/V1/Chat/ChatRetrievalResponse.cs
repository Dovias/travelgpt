namespace TravelGPT.Server.Dtos.Api.V1.Chat;

public readonly record struct ChatRetrievalResponse
{
    public required IEnumerable<ChatMessageResponseRetrievalResponse> Messages { get; init; }
}

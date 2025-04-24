namespace TravelGPT.Server.Dtos.Api.V1;

public readonly record struct ChatResponse
{
    public required IEnumerable<ChatMessageResponse> Messages { get; init; }
}

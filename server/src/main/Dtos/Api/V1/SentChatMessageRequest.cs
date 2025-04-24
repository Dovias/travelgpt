namespace TravelGPT.Server.Dtos.Api.V1;

public readonly record struct SentChatMessageRequest
{
    public required string Text { get; init; }
}

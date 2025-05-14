namespace TravelGPT.Server.Dtos.Chat;

public readonly record struct ChatCreationRequest
{
    public required string Text { get; init; }
}

namespace TravelGPT.Server.Dtos.Chat;

public readonly record struct ChatCreationResponse
{
    public required Guid Id { get; init; }
    public required string Text { get; init; }
}

namespace TravelGPT.Server.Models.Chat;

public readonly record struct ChatMessage
{
    public required string Text { get; init; }
}

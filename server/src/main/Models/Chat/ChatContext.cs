namespace TravelGPT.Server.Models.Chat;

public readonly record struct ChatContext
{
    public required Guid Id { get; init; }
    public required IChat Messages { get; init; }
}

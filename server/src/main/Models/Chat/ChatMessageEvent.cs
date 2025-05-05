namespace TravelGPT.Server.Models.Chat;

public readonly record struct ChatMessageEvent
{
    public required ChatContext Chat { get; init; }
    public required ChatMessageContext Message { get; init; }
}

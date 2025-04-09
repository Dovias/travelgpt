namespace TravelGPT.Models.Chat.InMemory;

public record InMemoryUserChatMessage : InMemoryChatMessage, IUserChatMessage
{
    public required IUserChatContext User { get; init; }
}

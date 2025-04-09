namespace TravelGPT.Models.Chat;

public interface IChatMessage : IMessage
{
    DateTime CreatedAt { get; }
}

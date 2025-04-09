namespace TravelGPT.Models.Chat;

public interface IUserChatMessage : IChatMessage
{
    IUserChatContext User { get; }
}

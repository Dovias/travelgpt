namespace TravelGPT.Server.Models.Chat;

public interface IChatMessageContext
{
    IChat Chat { get; }
    IChatMessage Message { get; }
}

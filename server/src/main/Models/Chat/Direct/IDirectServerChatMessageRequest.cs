namespace TravelGPT.Server.Models.Chat.Direct;

public interface IDirectServerChatMessageResponse
{
    IDirectServerChatMessage Sent { get; }
    IDirectServerChatMessage Received { get; }
}

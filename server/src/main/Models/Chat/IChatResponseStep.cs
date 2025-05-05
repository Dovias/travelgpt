namespace TravelGPT.Server.Models.Chat;

public interface IChatResponseStep
{
    bool Step(ChatContext chat, ChatMessageContext sent, ref string response);
}

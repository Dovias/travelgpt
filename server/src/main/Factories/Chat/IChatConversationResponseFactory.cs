using TravelGPT.Server.Models.Chat;

namespace TravelGPT.Server.Factories.Chat;

public interface IChatConversationResponseFactory
{
    string GetChatResponse(IEnumerable<ChatConversation> conversations, string message);
}

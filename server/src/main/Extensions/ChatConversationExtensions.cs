using TravelGPT.Server.Models.Chat;

namespace TravelGPT.Server.Extensions;

public static class ChatConversationExtensions
{
    public static IEnumerable<string> TakeMessageAndResponse(this ChatConversation conversation)
    {
        yield return conversation.Message;
        yield return conversation.Response;
    }
}

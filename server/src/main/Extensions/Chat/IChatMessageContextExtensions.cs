using TravelGPT.Server.Models.Chat;

namespace TravelGPT.Server.Extensions.Chat;

public static class IChatUserContextExtensions
{
    public static IChatMessageContext SendMessage(this IChatParticipantContext context, string message)
    {
        return context.SendMessage(new ChatMessage(message));
    }
}

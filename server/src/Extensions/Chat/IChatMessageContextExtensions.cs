using TravelGPT.Models.Chat;

namespace TravelGPT.Extensions.Chat;

public static class IChatUserContextExtensions
{
    public static IChatMessageContext SendMessage(this IChatParticipantContext context, string message)
    {
        return context.SendMessage(new ChatMessage(message));
    }
}

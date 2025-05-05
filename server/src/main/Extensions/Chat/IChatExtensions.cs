using TravelGPT.Server.Models.Chat;
using TravelGPT.Server.Models.User;

namespace TravelGPT.Server.Extensions.Chat;

public static class IChatExtensions
{
    public static ChatMessageContext Add(this IChat chat, UserContext author, string text)
    {
        return chat.Add(author, new ChatMessage()
        {
            Text = text
        });
    }
}

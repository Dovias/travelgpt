using TravelGPT.Server.Models.Chat;
using TravelGPT.Server.Models.Chat.InMemory;

namespace TravelGPT.Server.Extensions.Chat;

public static class IChatMessageContainerExtensions
{
    public static void Add(this IChat messages, int author, string message)
    {
        messages.Add(new InMemoryChatMessageDetails()
        {
            Author = new InMemoryChatParticipant
            {
                Id = author
            },
            Text = message
        }
        );
    }

    public static void Add(this IChat messages, IChatParticipant author, string message) => Add(messages, author.Id, message);
}

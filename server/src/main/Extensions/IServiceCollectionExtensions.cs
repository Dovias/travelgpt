using System.Reactive.Subjects;
using TravelGPT.Server.Models.Chat;
using TravelGPT.Server.Models.Chat.Direct;
using TravelGPT.Server.Models.Chat.InMemory;

namespace TravelGPT.Server.Extensions;

public static class IServiceCollectionExtensions
{
    private static readonly int clientId = 0;
    private static readonly int serverId = 1;

    private static ISubject<(IChat, IChatMessage)> ConstructSubject(IEnumerable<IDirectServerChatResponseStep> steps, string defaultResponse)
    {
        Subject<(IChat, IChatMessage)> subject = new();
        subject.Subscribe(context =>
        {
            var (chat, message) = context;
            if (message.AuthorId == serverId) return;

            string response = defaultResponse;
            foreach (IDirectServerChatResponseStep step in steps)
            {
                if (!step.Step(new DirectServerChatFacade(chat, clientId, subject), new InMemoryDirectServerChatMessage
                {
                    Id = message.Id,
                    Text = message.Text,
                    Author = message.AuthorId == clientId ? DirectServerChatMessageAuthor.Client : DirectServerChatMessageAuthor.Server,
                    Created = DateTime.Now
                }, ref response)) break;
            }
            subject.OnNext((chat, chat.Add(serverId, response)));
        });

        return subject;
    }

    private static IDirectServerChatRepository ConstructRepository(IServiceProvider provider, IEnumerable<IDirectServerChatResponseStep> steps, string defaultResponse)
        => new DirectServerChatRepositoryFacade(
            new InMemoryChatRepository(new Dictionary<int, IChat>(), new Dictionary<int, IChatMessage>()),
            clientId,
            ConstructSubject(steps, defaultResponse)
        );

    public static IServiceCollection AddDirectServerChatRepository(this IServiceCollection collection, ICollection<IDirectServerChatResponseStep> steps, string defaultResponse = "")
        => collection.AddSingleton(provider => ConstructRepository(provider, steps, defaultResponse));
}

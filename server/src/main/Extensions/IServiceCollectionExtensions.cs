using System.Reactive.Subjects;
using TravelGPT.Server.Models.Chat;
using TravelGPT.Server.Models.Chat.Response;
using TravelGPT.Server.Models.Llm;
using TravelGPT.Server.Models.User;
using TravelGPT.Server.Services.Chat;

namespace TravelGPT.Server.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddDirectChat(this IServiceCollection collection)
    => collection.AddSingleton<IDirectChatService>(provider =>
    {
        UserContext client = new() { Id = 0 };
        UserContext server = new() { Id = 1 };

        Subject<ChatMessageEvent> subject = new();

        IEnumerable<IChatResponseStep> steps = [
            new LlmChatResponseStep(new GeminiLlmClient(
                new HttpClient(),
                provider.GetRequiredService<IConfiguration>()["GeminiApiKey"]
                    ?? throw new KeyNotFoundException("Missing Gemini API key")
            ), server, [])
        ];
        subject.Subscribe(@event =>
        {
            ChatContext chat = @event.Chat;
            ChatMessageContext message = @event.Message;

            if (message.Author.Id == server.Id) return;

            string response = "";
            foreach (IChatResponseStep step in steps)
            {
                if (!step.Step(chat, message, ref response)) break;
            }
            subject.OnNext(chat, chat.Messages.Add(server, response));

        });

        return new DirectServerChatService(
            new InMemoryChatRepository(
                new Dictionary<int, ChatContext>(),
                new Dictionary<int, ChatMessageContext>()
            ),
            client, server, subject
        );
    });
}

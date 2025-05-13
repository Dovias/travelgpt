using System.Reactive.Subjects;
using System.Text.Json;
using TravelGPT.Server.Extensions.Chat;
using TravelGPT.Server.Models.Chat;
using TravelGPT.Server.Models.Chat.Response;
using TravelGPT.Server.Models.Chat.Dictionary;
using TravelGPT.Server.Models.Llm.Gemini;
using TravelGPT.Server.Models.Llm.Gemini.Json;
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

        JsonSerializerOptions options = new();
        options.Converters.Add(new GeminiLlmRequestJsonConverter());
        options.Converters.Add(new GeminiLlmResponseEnumerableJsonConverter());

        IEnumerable<IChatResponseStep> steps = [
            new LlmChatResponseStep(new GeminiLlmClient(
                new HttpClient(),
                options,
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
            new DictionaryChatRepository(new Dictionary<Guid, ChatContext>()),
            client, server, subject
        );
    });
}

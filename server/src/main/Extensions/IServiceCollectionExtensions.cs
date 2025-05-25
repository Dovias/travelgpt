using System.Reactive.Subjects;
using TravelGPT.Server.Extensions.Chat;
using TravelGPT.Server.Models.Chat;
using TravelGPT.Server.Models.Chat.Response;
using TravelGPT.Server.Models.Chat.Dictionary;
using TravelGPT.Server.Models.Llm.Gemini;
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
                provider.GetRequiredService<IConfiguration>()["GEMINI_API_KEY"]
                    ?? throw new KeyNotFoundException("Missing Gemini API key")
            ), server, [
"You are a travel guide, help the user plan its trip based on provided input.",
"Provide user with recommendations on some places to visit, dine, or stay during the trip.",
"Provided trip information must be in bulletpoint format.",
"If user answers something that is not related with travel context, just repeat the provided questions until it answers them in correct fashion.",
@"You are required to ask these things in order, one after another:
1. Were user should travel to (like country, city or route),
2. Whether user has any more information that might correlate with the trip."
            ])
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

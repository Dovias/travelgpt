namespace TravelGPT.Server.Extensions;

public static class IServiceCollectionExtensions
{
    /*
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
"For each dining place, accommodation and else provide a link to its website, if availablethe links must be all encased in [[link]].",
@"You are required to ask these things in order, one after another:
1. Were user should travel to (like country, city or route),
2. Whether user has any more information that might correlate with the trip, give examples what user needs to provide."
            ])
        ];

        subject.Subscribe(@event =>
        {
            ChatConversationContext chat = @event.Chat;
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
            new DictionaryChatRepository(new Dictionary<Guid, ChatConversationContext>()),
            client, server, subject
        );
    });
    */
}

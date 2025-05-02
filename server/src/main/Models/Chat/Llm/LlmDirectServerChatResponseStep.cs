using TravelGPT.Server.Models.Chat.Direct;
using TravelGPT.Server.Models.Chat.Llm;

namespace TravelGPT.Server.Models.Chat.Response;

public class LlmDirectServerChatResponseStep : IDirectServerChatResponseStep
{
    public required ILlmClient Client { get; init; }
    public required IEnumerable<string> Instructions { get; init; }

    public bool Step(IDirectServerChat chat, IDirectServerChatMessage sent, ref string response)
    {
        response = Client.Fetch(new InMemoryLlmRequest()
        {
            Instructions = Instructions,
            Messages =
                from message in chat
                select (ILlmMessage)new InMemoryLlmMessage()
                {
                    Text = message.Text,
                    Author = message.Author
                }
        }).Text;
        return true;
    }
}
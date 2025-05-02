using TravelGPT.Server.Models.Chat.Direct;

namespace TravelGPT.Server.Models.Chat.Llm;

public interface ILlmMessage
{
    string Text { get; }
    DirectServerChatMessageAuthor Author { get; }
}

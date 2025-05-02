namespace TravelGPT.Server.Models.Chat.Llm;

public interface ILlmRequest
{
    public IEnumerable<string> Instructions { get; }
    public IEnumerable<ILlmMessage> Messages { get; }
}

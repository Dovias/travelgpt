namespace TravelGPT.Server.Models.Chat.Llm;

public interface ILlmClient
{
    ILlmResponse Fetch(ILlmRequest request);
}

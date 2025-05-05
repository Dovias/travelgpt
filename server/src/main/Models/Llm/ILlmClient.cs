namespace TravelGPT.Server.Models.Llm;

public interface ILlmClient
{
    LlmResponse Fetch(LlmRequest request);
}

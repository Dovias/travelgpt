namespace TravelGPT.Server.Models.Llm;

public interface ILlmClient
{
    LlmResponse FetchResponse(LlmRequest request);
}

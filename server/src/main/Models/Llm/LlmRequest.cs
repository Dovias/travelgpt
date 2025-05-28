namespace TravelGPT.Server.Models.Llm;

public readonly record struct LlmRequest(IEnumerable<string> Instructions, IEnumerable<LlmMessage> Messages);

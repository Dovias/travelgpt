using System.Text.Json;

namespace TravelGPT.Server.Models.Llm.Gemini;

public class GeminiLlmClient(HttpClient client, JsonSerializerOptions options, string apiKey) : ILlmClient
{
    public LlmResponse Fetch(LlmRequest request)
    {
        HttpResponseMessage message = client.PostAsJsonAsync(
            $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}",
            request,
            options
        ).Result.EnsureSuccessStatusCode();

        IEnumerable<LlmResponse> candidates = message.Content.ReadFromJsonAsync<IEnumerable<LlmResponse>>(options).Result!;
        return new LlmResponse
        {
            Text = candidates.First().Text
        };
    }
}

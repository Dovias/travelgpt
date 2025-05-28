using System.Text.Json;
using TravelGPT.Server.Models.Llm.Gemini.Json;

namespace TravelGPT.Server.Models.Llm.Gemini;

public class GeminiLlmClient(string apiKey) : ILlmClient
{
    private readonly string _apiKey = apiKey;
    private readonly HttpClient _client = new();
    private readonly JsonSerializerOptions _options = new()
    {
        Converters = {
            new GeminiLlmRequestJsonConverter(),
            new GeminiLlmResponseEnumerableJsonConverter()
        }
    };

    public LlmResponse FetchResponse(LlmRequest request)
    {
        HttpResponseMessage message = _client.PostAsJsonAsync(
            $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={_apiKey}",
            request,
            _options
        ).Result.EnsureSuccessStatusCode();

        IEnumerable<LlmResponse> candidates = message.Content.ReadFromJsonAsync<IEnumerable<LlmResponse>>(_options).Result!;
        return new LlmResponse
        {
            Text = candidates.First().Text
        };
    }
}

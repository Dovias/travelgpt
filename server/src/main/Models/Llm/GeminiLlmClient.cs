using System.Text.Json;

namespace TravelGPT.Server.Models.Llm;

public readonly struct GeminiLlmClient(HttpClient client, string apiKey) : ILlmClient
{
    public LlmResponse Fetch(LlmRequest request)
    {
        JsonSerializerOptions options = new();
        options.Converters.Add(new GeminiLlmRequestJsonConverter());

        HttpResponseMessage response = client.PostAsJsonAsync(
            $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}",
            request,
            options
        ).Result.EnsureSuccessStatusCode();

        var data = response.Content.ReadFromJsonAsync<dynamic>().Result!;
        return new LlmResponse
        {
            Text = data.GetProperty("candidates")[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString()
        };
    }
}

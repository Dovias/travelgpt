using System.Text.Json;

namespace TravelGPT.Server.Models.Chat.Llm;

public readonly struct GeminiLlmClient : ILlmClient
{
    public required HttpClient HttpClient { get; init; }
    public required string ApiKey { get; init; }

    public ILlmResponse Fetch(ILlmRequest request)
    {
        JsonSerializerOptions options = new();
        options.Converters.Add(new GeminiLlmRequestJsonConverter());

        HttpResponseMessage response = HttpClient.PostAsJsonAsync(
            $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={ApiKey}",
            request,
            options
        ).Result.EnsureSuccessStatusCode();

        var data = response.Content.ReadFromJsonAsync<dynamic>().Result!;
        return new InMemoryLlmResponse
        {
            Text = data.GetProperty("candidates")[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString()
        };
    }
}

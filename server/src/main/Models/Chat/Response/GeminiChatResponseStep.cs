namespace TravelGPT.Server.Models.Chat.Response;

public class GeminiChatResponseStep(HttpClient httpClient, string apiKey) : IChatResponseStep
{
    public bool Step(IChat chat, IChatMessage sent, ref string response)
    {
        var payload = new
        {
            contents = new[] {
                new {
                    parts = (from message in chat select new { message.Text }).ToArray()
                }
            }
        };

        var json = httpClient.PostAsJsonAsync(
            $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}",
            payload
        ).Result;

        if (!json.IsSuccessStatusCode)
        {
            return false;
        }

        var data = json.Content.ReadFromJsonAsync<dynamic>().Result!;
        response = data.GetProperty("candidates")[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString();
        return true;
    }
}
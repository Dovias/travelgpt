
using static TravelGPT.Services.Chat.IChat;

namespace TravelGPT.Services.Chat.Gemini;

public static class GeminiChat
{
    private static readonly HttpClient s_client = new();
    private static readonly string s_geminiApiKey = "";
    private static readonly Guid s_Id = Guid.NewGuid();

    public static async void ReplyToMessage(MessageContext context)
    {
        if (context.Message.Author == s_Id)
        {
            return;
        }

        var payload = new
        {
            contents = new[] {
            new {
                parts = new[] {
                    new {
                        context.Message.Text
                    }
                }
            }
        }
        };

        var response = await s_client.PostAsJsonAsync(
            $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={s_geminiApiKey}",
            payload
        );
        if (response.IsSuccessStatusCode)
        {
            var data = (await response.Content.ReadFromJsonAsync<dynamic>())!;
            context.Chat.AddMessage(new Message(s_Id, data.GetProperty("candidates")[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString()));
        }

    }
}
using TravelGPT.Server.Extensions.Chat;
using TravelGPT.Server.Models.Chat;

namespace TravelGPT.Server.Observers.Chat;

public class GeminiChatObserver(HttpClient httpClient, string apiKey, IChatParticipant author) : IObserver<IChatMessageContext>
{
    public void OnCompleted() { }

    public void OnError(Exception error) { }

    public async void OnNext(IChatMessageContext context)
    {
        if (context.Message.Author.Id == author.Id)
        {
            return;
        }

        var payload = new
        {
            contents = new[] {
                new {
                    parts = (from message in context.Chat select new { message.Text }).ToArray()
                }
            }
        };

        var response = await httpClient.PostAsJsonAsync(
            $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}",
            payload
        );

        if (!response.IsSuccessStatusCode)
        {
            return;
        }

        var data = (await response.Content.ReadFromJsonAsync<dynamic>())!;
        context.Chat.Add(author, (string)data.GetProperty("candidates")[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString());
    }
}
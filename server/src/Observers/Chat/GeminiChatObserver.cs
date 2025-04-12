
using TravelGPT.Models.Chat;
using TravelGPT.Extensions.Chat;

namespace TravelGPT.Observers.Chat;

public class GeminiChatObserver(HttpClient httpClient, string apiKey) : IObserver<IChatMessageContext>
{
    public required IUserChatContext User { get; init; }

    public void OnCompleted() { }

    public void OnError(Exception error) { }

    public async void OnNext(IChatMessageContext context)
    {
        if (context.User.Id == User.Id)
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

        var response = await httpClient.PostAsJsonAsync(
            $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}",
            payload
        );

        if (!response.IsSuccessStatusCode)
        {
            return;
        }

        var data = (await response.Content.ReadFromJsonAsync<dynamic>())!;
        User.SendMessage((string)data.GetProperty("candidates")[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString());
    }
}
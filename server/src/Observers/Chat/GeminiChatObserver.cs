
using TravelGPT.Models.Chat;

namespace TravelGPT.Observers.Chat;

public class GeminiChatObserver(HttpClient httpClient, string apiKey) : IObserver<IUserChatMessageContext>
{
    public required IUserChatContext User { get; init; }

    public void OnCompleted() { }

    public void OnError(Exception error) { }

    public async void OnNext(IUserChatMessageContext context)
    {
        if (context.Message.User.Id == User.Id)
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
        User.SendMessage(data.GetProperty("candidates")[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString());
    }
}
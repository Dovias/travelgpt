
using TravelGPT.Models.Chat;

namespace TravelGPT.Observers.Chat;

public class GeminiChatObserver(HttpClient httpClient, string apiKey) : IObserver<MessageContext>
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly string _apiKey = apiKey;

    public required Guid Id { get; init; }

    public void OnCompleted() { }

    public void OnError(Exception error) { }

    public async void OnNext(MessageContext context)
    {
        if (context.Message.Author == Id)
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

        var response = await _httpClient.PostAsJsonAsync(
            $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={_apiKey}",
            payload
        );

        if (!response.IsSuccessStatusCode)
        {
            return;
        }

        var data = (await response.Content.ReadFromJsonAsync<dynamic>())!;
        context.Chat.AddMessage(new Message(Id, data.GetProperty("candidates")[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString()));
    }
}
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace TravelGPT.Server.Models.Llm;

public class GeminiLlmRequestJsonConverter : JsonConverter<LlmRequest>
{
    public override LlmRequest Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument document = JsonDocument.ParseValue(ref reader);
        return new()
        {
            Messages =
                from content in document.RootElement.GetProperty("contents").EnumerateArray()
                from part in content.GetProperty("parts").EnumerateArray()
                select new LlmMessage()
                {
                    Text = part.GetProperty("text").GetString()!,
                    Role = part.GetProperty("role").GetString() == "model" ? LlmMessageRole.Model : LlmMessageRole.User
                },

            Instructions =
                from part in document.RootElement.TryGetProperty("system_instruction", out JsonElement element)
                    ? element.GetProperty("parts").EnumerateArray()
                    : []
                select part.GetProperty("text").GetString()
        };
    }

    public override void Write(Utf8JsonWriter writer, LlmRequest value, JsonSerializerOptions options)
    {
        JsonObject root = [
            new KeyValuePair<string, JsonNode?>("contents", new JsonArray([..
                from message in value.Messages
                select new JsonObject([
                    new KeyValuePair<string, JsonNode?>("role", message.Role == LlmMessageRole.User ? "user" : "model"),
                    new KeyValuePair<string, JsonNode?>("parts", new JsonArray([
                        new JsonObject([
                            new KeyValuePair<string, JsonNode?>("text", message.Text)
                        ])
                    ]))
                ])
            ]))
        ];

        if (value.Instructions.Any())
        {
            root.Add("system_instruction", new JsonObject([
                new KeyValuePair<string, JsonNode?>("parts", new JsonArray([..
                    from instruction in value.Instructions
                    select new JsonObject([
                        new KeyValuePair<string, JsonNode?>("text", instruction)
                    ])
                ]))
            ]));
        }

        root.WriteTo(writer);
    }

}

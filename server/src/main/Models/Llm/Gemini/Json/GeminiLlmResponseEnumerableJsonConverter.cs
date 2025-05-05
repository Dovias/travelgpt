using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace TravelGPT.Server.Models.Llm.Gemini.Json;

public class GeminiLlmResponseEnumerableJsonConverter : JsonConverter<IEnumerable<LlmResponse>>
{
    public override IEnumerable<LlmResponse> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument document = JsonDocument.ParseValue(ref reader);

        return [.. document.RootElement.GetProperty("candidates").EnumerateArray().Select(candidate => new LlmResponse() {
            Text = [..
                candidate.GetProperty("content").GetProperty("parts").EnumerateArray()
                    .Select(element => element.GetProperty("text").GetString()!)
            ]
        })];
    }

    public override void Write(Utf8JsonWriter writer, IEnumerable<LlmResponse> response, JsonSerializerOptions options)
    {
        JsonObject root = [new KeyValuePair<string, JsonNode?>("candidates", new JsonArray([..
            response.Select(candidate => new JsonObject() {
                new KeyValuePair<string, JsonNode?>("content", new JsonObject([
                    new KeyValuePair<string, JsonNode?>("parts", new JsonArray([..
                        candidate.Text.Select(text => new JsonObject([
                            new KeyValuePair<string, JsonNode?>("text", text)
                        ]))
                    ]))
                ]))
            })
        ]))];

        root.WriteTo(writer);
    }

}

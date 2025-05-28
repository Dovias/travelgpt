using TravelGPT.Server.Extensions;
using TravelGPT.Server.Models.Chat;
using TravelGPT.Server.Models.Llm;

namespace TravelGPT.Server.Factories.Chat;

public class TravelChatConversationResponseFactory(ILlmClient client) : IChatConversationResponseFactory
{
    private readonly ILlmClient _client = client;

    public string GetChatResponse(IEnumerable<ChatConversation> conversations, string message)
    {
        LlmResponse response = _client.FetchResponse(new LlmRequest([
            "You are a travel guide, help the user plan its trip based on provided input.",
            "Provide user with recommendations on some places to visit, dine, or stay during the trip.",
            "Provided trip information must be in bulletpoint format.",
            "If user answers something that is not related with travel context, just repeat the provided questions until it answers them in correct fashion.",
            @"You are required to ask these things in order, one after another:
            1. Were user should travel to (like country, city or route),
            2. Whether user has any more information that might correlate with the trip."
        ], [..
            conversations.SelectMany(conversation => conversation.TakeMessageAndResponse()).Append(message).Select((message, index) => new LlmMessage() {
                Text = message,
                Role = index % 2 == 0 ? LlmMessageRole.User : LlmMessageRole.Model
            })
        ]));

        return response.Text.Aggregate((accumulator, value) => $"{accumulator}\n{value}").ToString();
    }

}

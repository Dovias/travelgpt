using TravelGPT.Server.Models.Llm;
using TravelGPT.Server.Models.User;

namespace TravelGPT.Server.Models.Chat.Response;

public class LlmChatResponseStep(ILlmClient client, UserContext server, IEnumerable<string> instructions) : IChatResponseStep
{
    public bool Step(ChatContext chat, ChatMessageContext sent, ref string response)
    {
        response = client.Fetch(new LlmRequest()
        {
            Messages = chat.Messages.Select(context => new LlmMessage()
            {
                Text = context.Details.Text,
                Role = context.Id == server.Id ? LlmMessageRole.Model : LlmMessageRole.User
            }),
            Instructions = instructions
        }).Text.Aggregate((accumulator, value) => $"{accumulator}\n{value}").ToString();
        return true;
    }
}
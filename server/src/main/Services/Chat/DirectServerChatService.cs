using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using TravelGPT.Server.Dtos.Chat;
using TravelGPT.Server.Extensions.Chat;
using TravelGPT.Server.Models.Chat;
using TravelGPT.Server.Models.User;

namespace TravelGPT.Server.Services.Chat;

public class DirectServerChatService(IChatRepository repository, UserContext client, UserContext server, ISubject<ChatMessageEvent> subject) : IDirectChatService
{
    private ChatMessageEvent SendChatMessage(ChatContext chat, string text) {
        ChatMessageContext message = chat.Messages.Add(client, text);

        Task<ChatMessageEvent> task = subject
            .FirstAsync(@event => @event.Chat.Id == chat.Id && @event.Message.Author.Id == server.Id)
            .ToTask();

        subject.OnNext(chat, message);

        return task.Result;
    }

    public ChatCreationResponse CreateChat(ChatCreationRequest request)
    {
        ChatContext chat = repository.Create();
        return new()
        {
            Id = chat.Id,
            Text = SendChatMessage(chat, request.Text).Message.Details.Text
        };
    }

    public bool TryGetChatResponse(Guid id, out ChatRetrievalResponse response)
    {
        if (!repository.TryGet(id, out ChatContext chat))
        {
            response = default;
            return false;
        }
        
        response = new()
        {
            Messages = chat.Messages.Select(context => context.Details.Text)
        };
        return true; 
    }

    public bool DeleteChat(Guid id) => repository.Delete(id);

    public bool TryGetChatMessageResponse(Guid id, ChatMessageRetrievalRequest request, out ChatMessageRetrievalResponse response)
    {
        if (!repository.TryGet(id, out ChatContext chat))
        {
            response = default;
            return false;
        }

        response = new() { Text = SendChatMessage(chat, request.Text).Message.Details.Text };
        return true;
    }
}

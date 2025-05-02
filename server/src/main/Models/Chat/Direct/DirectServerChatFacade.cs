using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using TravelGPT.Server.Models.Chat.InMemory;

namespace TravelGPT.Server.Models.Chat.Direct;

public class DirectServerChatFacade(IChat chat, int authorId, ISubject<(IChat Chat, IChatMessage Message)> subject) : IDirectServerChat
{
    public int Id => chat.Id;

    private IDirectServerChatMessage ConstructMessage(IChatMessage message)
        => new InMemoryDirectServerChatMessage
        {
            Id = message.Id,
            Text = message.Text,
            Author = message.AuthorId == authorId ? DirectServerChatMessageAuthor.Client : DirectServerChatMessageAuthor.Server,
            Created = DateTime.Now
        };

    public IDirectServerChatMessageResponse Add(string text)
    {
        IChatMessage requestMessage = chat.Add(authorId, text);
        Task<(IChat Chat, IChatMessage Message)> task = subject.Where(context => chat.Id == context.Chat.Id).FirstAsync().ToTask();

        subject.OnNext((chat, requestMessage));
        IChatMessage responseMessage = task.Result.Message;

        return new InMemoryDirectServerChatMessageResponse
        {
            Sent = ConstructMessage(requestMessage),
            Received = ConstructMessage(responseMessage)
        };
    }

    public bool Contains(int id) => chat.Contains(id);

    public bool Remove(int id) => chat.Remove(id);

    public bool TryGet(int id, [NotNullWhen(true)] out IDirectServerChatMessage? message)
    {
        if (!chat.TryGet(id, out IChatMessage? rawMessage))
        {
            message = null;
            return false;
        }
        message = ConstructMessage(rawMessage);
        return true;
    }

    public IDirectServerChatMessage this[int id] => ConstructMessage(chat[id]);

    public IEnumerator<IDirectServerChatMessage> GetEnumerator() => (from message in chat select ConstructMessage(message)).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => chat.GetEnumerator();
}

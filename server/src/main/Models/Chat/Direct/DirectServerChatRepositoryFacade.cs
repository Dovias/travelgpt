using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Subjects;

namespace TravelGPT.Server.Models.Chat.Direct;

public class DirectServerChatRepositoryFacade(IChatRepository repository, int authorId, ISubject<(IChat Chat, IChatMessage Message)> subject) : IDirectServerChatRepository
{
    private IDirectServerChat ConstructChat(IChat chat) => new DirectServerChatFacade(chat, authorId, subject);

    public IDirectServerChat Create() => ConstructChat(repository.Create());

    public bool Delete(int id) => repository.Delete(id);

    public bool Contains(int id) => repository.Contains(id);

    public bool TryGet(int id, [NotNullWhen(true)] out IDirectServerChat? chat)
    {
        if (!repository.TryGet(id, out IChat? rawChat))
        {
            chat = null;
            return false;
        }
        chat = ConstructChat(rawChat);
        return true;
    }

    public IEnumerator<IDirectServerChat> GetEnumerator() => (from chat in repository select ConstructChat(chat)).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

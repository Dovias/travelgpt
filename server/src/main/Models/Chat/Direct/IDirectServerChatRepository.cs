using System.Diagnostics.CodeAnalysis;

namespace TravelGPT.Server.Models.Chat.Direct;

public interface IDirectServerChatRepository : IEnumerable<IDirectServerChat>
{
    IDirectServerChat Create();
    bool Delete(int id);

    bool TryGet(int id, [NotNullWhen(true)] out IDirectServerChat? chat);
    bool Contains(int id);
}

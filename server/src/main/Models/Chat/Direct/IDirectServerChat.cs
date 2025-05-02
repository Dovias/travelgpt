using System.Diagnostics.CodeAnalysis;

namespace TravelGPT.Server.Models.Chat.Direct;

public interface IDirectServerChat : IEnumerable<IDirectServerChatMessage>
{
    int Id { get; }

    IDirectServerChatMessageResponse Add(string text);
    bool Remove(int id);

    bool TryGet(int id, [NotNullWhen(true)] out IDirectServerChatMessage? message);
    bool Contains(int id);

    public IDirectServerChatMessage this[int id] { get; }
}

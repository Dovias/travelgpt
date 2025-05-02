using System.Diagnostics.CodeAnalysis;

namespace TravelGPT.Server.Models.Chat;

public interface IChat : IEnumerable<IChatMessage>
{
    int Id { get; }

    IChatMessage Add(int authorId, string text);
    bool Remove(int id);

    bool TryGet(int id, [NotNullWhen(true)] out IChatMessage? message);
    bool Contains(int id);

    public IChatMessage this[int id] { get; }
}

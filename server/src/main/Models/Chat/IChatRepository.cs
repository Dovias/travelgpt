using System.Diagnostics.CodeAnalysis;

namespace TravelGPT.Server.Models.Chat;

public interface IChatRepository : IEnumerable<IChat>
{
    IChat Create();
    bool Delete(int id);

    bool TryGet(int id, [NotNullWhen(true)] out IChat? chat);
    bool Contains(int id);
}

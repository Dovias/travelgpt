namespace TravelGPT.Server.Models.Chat;

public interface IChatRepository : IEnumerable<IChat>
{
    IChat Create();
    bool Delete(Guid id);

    bool TryGet(Guid id, out IChat? chat);
    bool Contains(Guid id);
}

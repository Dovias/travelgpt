namespace TravelGPT.Server.Models.Chat;

public interface IChatRepository : IEnumerable<IChat>
{
    IChat Create();
    bool Delete(int id);

    bool TryGet(int id, out IChat? chat);
    bool Contains(int id);
}

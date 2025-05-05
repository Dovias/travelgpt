namespace TravelGPT.Server.Models.Chat;

public interface IChatRepository : IEnumerable<ChatContext>
{
    ChatContext Create();
    bool Delete(int id);
    bool TryGet(int id, out ChatContext chat);
    bool Contains(int id);
}

namespace TravelGPT.Server.Models.Chat;

public interface IChatRepository : IEnumerable<ChatContext>
{
    ChatContext Create();
    bool Delete(Guid id);
    bool TryGet(Guid id, out ChatContext chat);
    bool Contains(Guid id);
}

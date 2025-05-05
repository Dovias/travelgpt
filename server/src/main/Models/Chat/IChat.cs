using TravelGPT.Server.Models.User;

namespace TravelGPT.Server.Models.Chat;

public interface IChat : IEnumerable<ChatMessageContext>
{
    ChatMessageContext Add(UserContext author, ChatMessage message);
    bool Remove(int id);

    bool TryGet(int id, out ChatMessageContext message);
    bool Contains(int id);

    public ChatMessageContext this[int id] { get; }
}

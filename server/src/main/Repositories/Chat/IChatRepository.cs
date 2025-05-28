using TravelGPT.Server.Models.Chat;

namespace TravelGPT.Server.Repositories.Chat;

public interface IChatRepository
{
    Guid CreateChat(ChatConversation conversation);
    bool DeleteChat(Guid chatId);

    void AddChatConversation(Guid chatId, ChatConversation conversation);
    IEnumerable<ChatConversation> FetchAllChatConversations(Guid chatId);
    IEnumerable<ChatConversation> FetchChatConversationsFromIndex(Guid chatId, int index);
    IEnumerable<ChatConversation> FetchChatConversationsUntilIndex(Guid chatId, int index);

    void ReplaceChatConversationsFromIndex(Guid chatId, int index, IEnumerable<ChatConversation> conversations);
    void DeleteChatConversationsFromIndex(Guid chatId, int index);
}

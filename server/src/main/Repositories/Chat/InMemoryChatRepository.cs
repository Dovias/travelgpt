
using TravelGPT.Server.Exceptions.Chat;
using TravelGPT.Server.Models.Chat;

namespace TravelGPT.Server.Repositories.Chat;

public class InMemoryChatRepository : IChatRepository
{
    private readonly Dictionary<Guid, List<ChatConversation>> _chats = [];

    public Guid CreateChat(ChatConversation conversation)
    {
        Guid chatId = Guid.NewGuid();

        _chats.Add(chatId, [conversation]);

        return chatId;
    }

    public bool DeleteChat(Guid chatId) => _chats.Remove(chatId);

    private List<ChatConversation> FetchChatConversationList(Guid chatId) =>
        _chats.TryGetValue(chatId, out List<ChatConversation>? conversations)
            ? conversations
            : throw new ChatNotFoundException(chatId);

    public void AddChatConversation(Guid chatId, ChatConversation conversation) => FetchChatConversationList(chatId).Add(conversation);

    public IEnumerable<ChatConversation> FetchAllChatConversations(Guid chatId) => FetchChatConversationList(chatId);

    public IEnumerable<ChatConversation> FetchChatConversationsFromIndex(Guid chatId, int index) => FetchAllChatConversations(chatId).Skip(index);

    public IEnumerable<ChatConversation> FetchChatConversationsUntilIndex(Guid chatId, int index) => FetchAllChatConversations(chatId).Take(index);

    private List<ChatConversation> FetchChatConversationList(Guid chatId, int chatConversationId)
    {
        List<ChatConversation> conversations = FetchChatConversationList(chatId);
        if (chatConversationId >= conversations.Count)
        {
            throw new ChatConversationNotFoundException(chatId, chatConversationId);
        }

        return conversations;
    }

    public void DeleteChatConversationsFromIndex(Guid chatId, int chatConversationId)
    {
        List<ChatConversation> conversations = FetchChatConversationList(chatId, chatConversationId);

        if (chatConversationId == 0)
        {
            DeleteChat(chatId);
            return;
        }

        conversations.RemoveRange(chatConversationId, conversations.Count - chatConversationId);
    }

    public void ReplaceChatConversationsFromIndex(Guid chatId, int chatConversationId, IEnumerable<ChatConversation> replacementConversations)
    {
        List<ChatConversation> conversations = FetchChatConversationList(chatId, chatConversationId);

        if (chatConversationId == 0 && !replacementConversations.Any())
        {
            DeleteChat(chatId);
            return;
        }

        conversations.RemoveRange(chatConversationId, conversations.Count - chatConversationId);
        conversations.AddRange(replacementConversations);
    }
}

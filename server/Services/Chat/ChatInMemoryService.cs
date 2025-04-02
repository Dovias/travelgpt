using TravelGPT.Models.Chat;

namespace TravelGPT.Services.Chat;

public class ChatInMemoryService : IChatService
{
    private readonly Func<Guid, IChat> _chatFactory;
    private readonly Dictionary<Guid, IChat> _chats = [];

    public ChatInMemoryService(Func<Guid, IChat> chatFactory)
    {
        _chatFactory = chatFactory;
    }

    private IChat CreateNewChat(IChat chat)
    {
        _chats[chat.Id] = chat;

        return chat;
    }

    private IChat CreateNewChat(Guid id)
    {
        return CreateNewChat(_chatFactory(id));
    }

    public IChat CreateNewChat()
    {
        return CreateNewChat(Guid.NewGuid());
    }

    public IChat GetExistingChat(Guid id)
    {
        return _chats[id];
    }
}
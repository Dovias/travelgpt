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

    private IChat CreateChat(IChat chat)
    {
        _chats[chat.Id] = chat;

        return chat;
    }

    private IChat CreateChat(Guid id)
    {
        return CreateChat(_chatFactory(id));
    }

    public IChat CreateChat()
    {
        return CreateChat(Guid.NewGuid());
    }

    public IChat GetChat(Guid id)
    {
        return _chats[id];
    }
}
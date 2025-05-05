using TravelGPT.Server.Dtos.Chat;

namespace TravelGPT.Server.Services.Chat;

public interface IDirectChatService
{
    ChatCreationResponse CreateChat();
    bool TryGetChatResponse(Guid id, out ChatRetrievalResponse response);
    bool DeleteChat(Guid id);
    bool TryGetChatMessageResponse(Guid chatId, ChatMessageRetrievalRequest request, out ChatMessageRetrievalResponse response);
}

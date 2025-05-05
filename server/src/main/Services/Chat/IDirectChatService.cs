using TravelGPT.Server.Dtos.Chat;

namespace TravelGPT.Server.Services.Chat;

public interface IDirectChatService
{
    ChatCreationResponse CreateChat();
    bool TryGetChatResponse(int id, out ChatRetrievalResponse response);
    bool DeleteChat(int id);
    bool TryGetChatMessageResponse(int chatId, ChatMessageResponseRetrievalRequest request, out ChatMessageResponseRetrievalResponse response);
}

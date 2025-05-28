namespace TravelGPT.Server.Exceptions.Chat;

public class ChatConversationNotFoundException(Guid chatId, int chatConversationId) : Exception($"Chat with ID {chatId} conversation with ID {chatConversationId} was not found");
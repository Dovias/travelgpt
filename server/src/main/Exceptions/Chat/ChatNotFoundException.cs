namespace TravelGPT.Server.Exceptions.Chat;

public class ChatNotFoundException(Guid chatId) : Exception($"Chat with ID {chatId} was not found");
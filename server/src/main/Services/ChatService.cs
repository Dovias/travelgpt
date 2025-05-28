using System.Diagnostics.CodeAnalysis;
using TravelGPT.Server.Exceptions.Chat;
using TravelGPT.Server.Extensions;
using TravelGPT.Server.Factories.Chat;
using TravelGPT.Server.Models.Chat;
using TravelGPT.Server.Repositories.Chat;

namespace TravelGPT.Server.Services;

public class ChatService(IChatRepository repository, IChatConversationResponseFactory factory)
{
    private readonly IChatRepository _repository = repository;
    private readonly IChatConversationResponseFactory _factory = factory;

    public Guid CreateChat(string message, out string response)
    {
        response = _factory.GetChatResponse([], message);
        return _repository.CreateChat(new(message, response));
    }

    public IEnumerable<string> FetchAllChatMessages(Guid chatId)
    {
        try
        {
            return _repository.FetchAllChatConversations(chatId).SelectMany(conversation => conversation.TakeMessageAndResponse());
        }
        catch (ChatNotFoundException)
        {
            return [];
        }
    }

    public bool TryDeleteChat(Guid chatId) => _repository.DeleteChat(chatId);

    public bool TrySend(Guid chatId, string message, [NotNullWhen(true)] out string? response)
    {
        try
        {
            response = _factory.GetChatResponse(_repository.FetchAllChatConversations(chatId), message);
            _repository.AddChatConversation(chatId, new(message, response));

        }
        catch (ChatNotFoundException)
        {
            response = null;
            return false;
        }
        return true;
    }

    public bool TryDeleteChatMessage(Guid chatId, int chatMessageId)
    {
        try
        {
            _repository.DeleteChatConversationsFromIndex(chatId, chatMessageId);
        }
        catch (Exception exception)
        {
            if (exception is ChatNotFoundException || exception is ChatConversationNotFoundException)
            {
                return false;
            }
            throw;
        }
        return true;
    }

    public bool TryEditChatMessage(Guid chatId, int chatMessageId, string message, [NotNullWhen(true)] out IEnumerable<string>? responses)
    {
        try
        {
            IEnumerable<ChatConversation> currentConversations = _repository.FetchAllChatConversations(chatId);

            List<ChatConversation> updatedConversations = [.. currentConversations.Take(chatMessageId)];
            updatedConversations.Add(new(message, _factory.GetChatResponse(updatedConversations, message)));

            foreach (ChatConversation currentConversation in currentConversations.Skip(chatMessageId + 1))
            {
                updatedConversations.Add(new(
                    currentConversation.Message,
                    _factory.GetChatResponse(updatedConversations, currentConversation.Message)
                ));
            }

            IEnumerable<ChatConversation> changedConversations = updatedConversations.Skip(chatMessageId);
            _repository.ReplaceChatConversationsFromIndex(chatId, chatMessageId, changedConversations);
            responses = changedConversations.Select(conversation => conversation.Response);
        }
        catch (Exception exception)
        {
            if (exception is ChatNotFoundException || exception is ChatConversationNotFoundException)
            {
                responses = null;
                return false;
            }
            throw;
        }

        return true;
    }
}

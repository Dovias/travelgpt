namespace TravelGPT.Services.Chat;

public interface IChatService
{
    IChat CreateNewChat();
    IChat GetExistingChat(Guid id);
}

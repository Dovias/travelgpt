namespace TravelGPT.Server.Models.Chat;

public interface IChatMessageDetails
{
    IChatParticipant Author { get; }

    string Text { get; }
}

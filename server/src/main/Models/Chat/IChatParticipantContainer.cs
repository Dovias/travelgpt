namespace TravelGPT.Server.Models.Chat;

public interface IChatParticipantContainer : IEnumerable<IChatParticipant>
{
    IChatParticipant Add();
    void Remove(int id);

    IChatParticipant? Get(int id);
    bool Contains(int id);
}

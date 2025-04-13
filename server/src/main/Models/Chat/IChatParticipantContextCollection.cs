namespace TravelGPT.Server.Models.Chat;

public interface IChatParticipantContextCollection : IEnumerable<IChatParticipantContext>, IObservable<IChatParticipantContext>
{
    IChatParticipantContext Add(int id);
    IChatParticipantContext? Get(int id);
}

namespace TravelGPT.Models.Chat;

public interface IChatMessageContextCollection : IEnumerable<IChatMessageContext>, IObservable<IChatMessageContext>
{
    IChatMessageContext? Get(int id);
}

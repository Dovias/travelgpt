namespace TravelGPT.Models.Chat;

public interface IChatContext : IObservable<IChatMessageContext>, IDisposable
{
    public int Id { get; }

    IEnumerable<IChatUserContext> Users { get; }
    IEnumerable<IChatMessageContext> Messages { get; }

    IChatUserContext AddUser(int id);
    IChatUserContext? GetUser(int id);
}

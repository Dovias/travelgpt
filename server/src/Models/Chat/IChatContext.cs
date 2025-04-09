namespace TravelGPT.Models.Chat;

public interface IChatContext : IDisposable
{
    public int Id { get; }
    public IChat Chat { get; }
}

namespace TravelGPT.Server.Models.Chat;

public interface IChatMessage
{
    int Id { get; }
    string Text { get; }

    int AuthorId { get; }
    DateTime Created { get; }
}

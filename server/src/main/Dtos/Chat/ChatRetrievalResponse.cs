namespace TravelGPT.Server.Dtos.Chat;

public readonly record struct ChatRetrievalResponse
{
    public required IEnumerable<string> Messages { get; init; }
}

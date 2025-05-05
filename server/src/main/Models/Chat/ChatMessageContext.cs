using TravelGPT.Server.Models.User;

namespace TravelGPT.Server.Models.Chat;

public readonly struct ChatMessageContext
{
    public required int Id { get; init; }
    public required ChatMessage Message { get; init; }
    public required UserContext Author { get; init; }
    public required DateTime Created { get; init; }
}

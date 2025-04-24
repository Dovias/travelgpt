namespace TravelGPT.Server.Models.Chat;

public interface IChatMessage : IChatMessageDetails
{
    int Id { get; }

    DateTime Created { get; }
}

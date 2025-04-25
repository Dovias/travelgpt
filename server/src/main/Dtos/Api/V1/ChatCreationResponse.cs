namespace TravelGPT.Server.Dtos.Api.V1;

public readonly record struct ChatCreationResponse
{
    public required Guid Id { get; init; }
}

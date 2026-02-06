namespace Movie.Api.Contracts.Responses;

public sealed record MovieResponse
{
    public required string Id { get; init; }
    public required string Name { get; init; } = string.Empty;
    public required DateOnly ReleaseDate { get; init; }

    public string? Description { get; init; }
    public required decimal Price { get; init; }
    public required string Genre { get; init; } = string.Empty;
}
namespace Movie.Api.Entities;

public sealed class Movie
{
    public required string Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public required DateOnly ReleaseDate { get; set; }

    public string? Description { get; set; }
    public required decimal Price { get; set; }
    public required string Genre { get; set; } = string.Empty;

    public required string ImageUri { get; set; } = string.Empty;
}
namespace Movie.Api.Contracts.Responses;

public sealed class MoviesResponse
{
    public IEnumerable<MovieResponse> Items { get; set; } = [];
}
using Microsoft.AspNetCore.Mvc;

namespace Movie.Api.Contracts.Queries;

public sealed class MoviesQueryParameter
{
    [FromQuery(Name = "q")]
    public string? Search { get; set; }
    public string? Genre { get; set; }
}
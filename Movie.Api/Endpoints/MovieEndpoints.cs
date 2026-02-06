using Movie.Api.Contracts.Responses;
using Movie.Api.Mappings;
using Movie.Api.Repositories.Interface;

namespace Movie.Api.Endpoints;

public static  class MovieEndpoints
{
    public static void MapMovieEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/movies");

        group.MapGet("/", async (IMovieRepository repo, CancellationToken ct) =>
        {
            var movies = await repo.GetAllAsync(ct);

            return Results.Ok(new MoviesResponse
            {
                Items = movies.Select(m => m.ToResponse()).ToList()
            });
        });
    }
}
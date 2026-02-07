using Microsoft.AspNetCore.Mvc;
using Movie.Api.Contracts.Queries;
using Movie.Api.Contracts.Requests;
using Movie.Api.Contracts.Responses;
using Movie.Api.Mappings;
using Movie.Api.Repositories.Interface;

namespace Movie.Api.Endpoints;

public static  class MovieEndpoints
{
    private const string GetMovieById = "GetMovieById";
    public static void MapMovieEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/movies");

        group.MapGet("/", async ([AsParameters] MoviesQueryParameter query ,IMovieRepository repo, CancellationToken ct) =>
        {
            var movies = await repo.GetAllAsync(query,ct);

            return Results.Ok(new MoviesResponse
            {
                Items = movies.Select(m => m.ToResponse()).ToList()
            });
        });

        group.MapGet("/{id}", async (string id, IMovieRepository repo, CancellationToken ct) =>
        {
            var movie = await repo.GetByIdAsync(id, ct);
            return movie is null
                ? Results.NotFound()
                : Results.Ok(movie.ToResponse());
            
        }).WithName(GetMovieById);

        group.MapPost("/", async (CreateMovieRequest request, IMovieRepository repo, CancellationToken ct) =>
        {
            var movie = request.ToEntity();
            await repo.AddAsync(movie, ct);
            
            return Results.CreatedAtRoute(GetMovieById, 
                new { id = movie.Id}, 
                movie.ToResponse()
                );
        });

        group.MapPut("/{id}", async (string id, UpdateMovieRequest request, IMovieRepository repo, CancellationToken ct) =>
        {
            var movie = await repo.GetByIdAsync(id, ct);

            if (movie is null)
            {
                return Results.NotFound();
            }
            
            movie.UpdateFrom(request);
            await repo.UpdateAsync(movie, ct);
            
            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (string id, IMovieRepository repo, CancellationToken ct) =>
        {
            await repo.DeleteAsync(id, ct);
            return Results.NoContent();
        });
    }
}
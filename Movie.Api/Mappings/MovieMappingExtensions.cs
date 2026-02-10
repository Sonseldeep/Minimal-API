using Movie.Api.Contracts.Requests;
using Movie.Api.Contracts.Responses;

namespace Movie.Api.Mappings;

public static class MovieMappingExtensions
{
    public static Entities.Movie ToEntity(this CreateMovieRequest request)
    {
        return new Entities.Movie
        {
            Id = $"m_{Guid.CreateVersion7()}",
            Name = request.Name,
            ReleaseDate = request.ReleaseDate,
            Description = request.Description,
            Price = request.Price,
            Genre = request.Genre,
            ImageUri = request.ImageUri
        };
    }

    public static MovieResponse ToResponse(this Entities.Movie response)
    {
        return new MovieResponse
        {
            Id = response.Id,
            Name = response.Name,
            ReleaseDate = response.ReleaseDate,
            Description = response.Description,
            Price = response.Price,
            Genre = response.Genre,
            ImageUri = response.ImageUri
        };
    }

    public static void UpdateFrom(this Entities.Movie entity, UpdateMovieRequest request)
    {
        entity.Name = request.Name;
        entity.ReleaseDate = request.ReleaseDate;
        entity.Description = request.Description;
        entity.Price = request.Price;
        entity.Genre = request.Genre;
        entity.ImageUri = request.ImageUri;
    }
}
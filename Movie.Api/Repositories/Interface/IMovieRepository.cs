using Movie.Api.Contracts.Queries;

namespace Movie.Api.Repositories.Interface;

public interface IMovieRepository
{
    Task<IEnumerable<Entities.Movie>> GetAllAsync(MoviesQueryParameter query ,CancellationToken ct = default);
    Task<Entities.Movie?> GetByIdAsync(string id, CancellationToken ct = default);
    Task AddAsync(Entities.Movie movie, CancellationToken ct = default);
    Task UpdateAsync(Entities.Movie movie, CancellationToken ct = default);
    Task DeleteAsync(string id, CancellationToken ct = default);
}
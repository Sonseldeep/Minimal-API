using Microsoft.EntityFrameworkCore;
using Movie.Api.Database;
using Movie.Api.Repositories.Interface;

namespace Movie.Api.Repositories.Implementations;

public sealed class MovieRepository(MovieDbContext context) : IMovieRepository
{
    public async Task<IEnumerable<Entities.Movie>> GetAllAsync(CancellationToken ct = default)
    {
        return await context.Movies
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<Entities.Movie?> GetByIdAsync(string id, CancellationToken ct = default)
    {
        return await context.Movies
            .FirstOrDefaultAsync(m => m.Id == id, ct);
    }

    public async Task AddAsync(Entities.Movie movie, CancellationToken ct = default)
    {
        context.Movies.Add(movie);
        await context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Entities.Movie movie, CancellationToken ct = default)
    {
        context.Movies.Update(movie);
        await context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(string id, CancellationToken ct = default)
    {
        await context.Movies
            .Where(m => m.Id == id)
            .ExecuteDeleteAsync(ct);
    }
}
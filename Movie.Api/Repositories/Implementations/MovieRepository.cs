using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie.Api.Contracts.Queries;
using Movie.Api.Database;
using Movie.Api.Repositories.Interface;

namespace Movie.Api.Repositories.Implementations;

public sealed class MovieRepository(MovieDbContext context) : IMovieRepository
{
    public async Task<IEnumerable<Entities.Movie>> GetAllAsync(MoviesQueryParameter query ,CancellationToken ct = default)
    {
        query.Search ??= query.Search?.Trim().ToLower();
        query.Genre ??= query.Genre?.Trim().ToLower();
        
        
        return await context.Movies
            .AsNoTracking()
            .Where(m => string.IsNullOrWhiteSpace(query.Search) 
            || m.Name.ToLower().Contains(query.Search)
            || m.Description !=null && m.Description.ToLower().Contains(query.Search)
            )
            .Where(m => string.IsNullOrWhiteSpace(query.Genre)
            || m.Genre.ToLower().Contains(query.Genre))
            .OrderBy(m => m.Name)
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
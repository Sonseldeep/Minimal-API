using Microsoft.EntityFrameworkCore;

namespace Movie.Api.Database;

public sealed class MovieDbContext(DbContextOptions<MovieDbContext> options) : DbContext(options)
{
  public DbSet<Entities.Movie> Movies => Set<Entities.Movie>();
}
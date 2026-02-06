using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Movie.Api.Contracts.Requests;

public sealed record UpdateMovieRequest
{
    [Required,MinLength(2),MaxLength(50)]
    public required string Name { get; init; } = string.Empty;
    
    [Required]
    public required DateOnly ReleaseDate { get; init; }

    public string? Description { get; init; }
    
    [Required,Range(1,1000)]
    [Precision(18,2)]
    public required decimal Price { get; init; }
    
    [Required]
    public required string Genre { get; init; } = string.Empty;
}
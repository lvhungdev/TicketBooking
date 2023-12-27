using System.ComponentModel.DataAnnotations;
using Domain.Movies.Models;

namespace Infrastructure.Storage.Entities;

public class MovieEntity : BaseEntity
{
    [MaxLength(255)] public string Title { get; set; } = null!;
    [MaxLength(1024)] public string? Description { get; set; }
    public int DurationInSecond { get; set; }
    public Genre Genre { get; set; } = Genre.Unset;

    public Movie MapToMovie()
    {
        return new Movie
        {
            Id = Id,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt,
            Title = Title,
            Description = Description,
            DurationInSecond = DurationInSecond,
            Genre = Genre,
        };
    }

    public static MovieEntity FromMovie(Movie movie)
    {
        return new MovieEntity
        {
            Id = movie.Id,
            CreatedAt = movie.CreatedAt,
            UpdatedAt = movie.UpdatedAt,
            Title = movie.Title,
            Description = movie.Description,
            DurationInSecond = movie.DurationInSecond,
            Genre = movie.Genre
        };
    }
}

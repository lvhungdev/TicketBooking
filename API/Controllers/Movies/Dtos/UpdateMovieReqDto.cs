using Domain.Movies.Models;

namespace API.Controllers.Movies.Dtos;

public class UpdateMovieReqDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public int DurationInSecond { get; set; }
    public Genre Genre { get; set; }

    public Movie MapToMovie()
    {
        return new Movie
        {
            Title = Title,
            Description = Description,
            DurationInSecond = DurationInSecond,
            Genre = Genre
        };
    }
}

using Domain.Common.Models;

namespace Domain.Movies.Models;

public class Movie : BaseModel
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public int DurationInSecond { get; set; }
    public Genre Genre { get; set; } = Genre.Unset;
}

public enum Genre
{
    Unset,
    Action,
    Drama,
    Romance
}

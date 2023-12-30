using Domain.Common.Models;
using Domain.Movies.Models;
using Domain.Theaters.Models;

namespace Domain.Showtimes.Models;

public class Showtime : BaseModel
{
    public Movie Movie { get; set; } = null!;
    public Theater Theater { get; set; } = null!;
    public Room Room { get; set; } = null!;
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public int Price { get; set; }
}

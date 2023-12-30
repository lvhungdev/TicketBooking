using Domain.Common.Models;

namespace Domain.Theaters.Models;

public class Room : BaseModel
{
    public string Title { get; set; } = null!;
    public List<Seat> Seats { get; set; } = new();
    public Theater Theater { get; set; } = null!;
}

using Domain.Models;

namespace Domain.Theaters.Models;

public class Theater : BaseModel
{
    public string Title { get; set; } = null!;
    public string Address { get; set; } = null!;
    public List<Room> Rooms { get; set; } = new();
}

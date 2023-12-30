using Domain.Common.Models;

namespace Domain.Theaters.Models;

public class Seat : BaseModel
{
    public string Title { get; set; } = null!;
    public Room Room { get; set; } = null!;
}

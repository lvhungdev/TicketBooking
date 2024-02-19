using Domain.Theaters.Models;

namespace API.Controllers.Theaters.Dtos;

public class RoomReqDto
{
    public string Title { get; set; } = null!;

    public Room MapToRoom()
    {
        return new Room { Title = Title };
    }
}

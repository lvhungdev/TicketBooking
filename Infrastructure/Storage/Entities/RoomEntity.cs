using System.ComponentModel.DataAnnotations;
using Domain.Theaters.Models;

namespace Infrastructure.Storage.Entities;

public class RoomEntity : BaseEntity
{
    [MaxLength(255)] public string Title { get; set; } = null!;

    public List<SeatEntity> Seats { get; } = new();

    [MaxLength(450)] public string TheaterId { get; set; } = null!;
    public TheaterEntity Theater { get; set; } = null!;

    public Room MapToRoom()
    {
        return new Room
        {
            Id = Id,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt,
            Title = Title,
            Seats = Seats.Select(m => m.MapToSeat()).ToList(),
            Theater = new Theater
            {
                Id = Theater.Id,
                CreatedAt = Theater.CreatedAt,
                UpdatedAt = Theater.UpdatedAt,
                Title = Theater.Title,
                Address = Theater.Address,
                Rooms = new List<Room>()
            },
        };
    }
}

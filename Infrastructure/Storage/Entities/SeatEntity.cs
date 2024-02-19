using System.ComponentModel.DataAnnotations;
using Domain.Theaters.Models;

namespace Infrastructure.Storage.Entities;

public class SeatEntity : BaseEntity
{
    [MaxLength(255)]
    public string Title { get; set; } = null!;

    [MaxLength(450)]
    public string RoomId { get; set; } = null!;
    public RoomEntity Room { get; set; } = null!;

    public Seat MapToSeat()
    {
        return new Seat
        {
            Id = Id,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt,
            Title = Title,
            Room = new Room
            {
                Id = Room.Id,
                CreatedAt = Room.CreatedAt,
                UpdatedAt = Room.UpdatedAt,
                Title = Room.Title,
                Seats = new List<Seat>(),
                Theater = null!
            }
        };
    }
}

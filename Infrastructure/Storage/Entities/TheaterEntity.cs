using System.ComponentModel.DataAnnotations;
using Domain.Theaters.Models;

namespace Infrastructure.Storage.Entities;

public class TheaterEntity : BaseEntity
{
    [MaxLength(255)] public string Title { get; set; } = null!;
    [MaxLength(255)] public string Address { get; set; } = null!;

    public List<RoomEntity> Rooms { get; } = new();

    public Theater MapToTheater()
    {
        return new Theater
        {
            Id = Id,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt,
            Title = Title,
            Address = Address,
            Rooms = Rooms.Select(m => m.MapToRoom()).ToList()
        };
    }

    public static TheaterEntity FromTheater(Theater theater)
    {
        return new TheaterEntity
        {
            Id = theater.Id,
            CreatedAt = theater.CreatedAt,
            UpdatedAt = theater.UpdatedAt,
            Title = theater.Title,
            Address = theater.Address
        };
    }
}

using Infrastructure.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Storage;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<MovieEntity> Movies { get; set; } = null!;

    public DbSet<TheaterEntity> Theaters { get; set; } = null!;
    public DbSet<RoomEntity> Rooms { get; set; } = null!;
    public DbSet<SeatEntity> Seats { get; set; } = null!;
}

using Infrastructure.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Storage;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<MovieEntity> Movies { get; set; } = null!;

    public DbSet<TheaterEntity> Theaters { get; set; } = null!;
    public DbSet<RoomEntity> Rooms { get; set; } = null!;
    public DbSet<SeatEntity> Seats { get; set; } = null!;
    public DbSet<UserEntity> Users { get; set; } = null!;
}

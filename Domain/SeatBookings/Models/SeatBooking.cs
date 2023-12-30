using Domain.Common.Models;
using Domain.Showtimes.Models;
using Domain.Theaters.Models;

namespace Domain.SeatBookings.Models;

public class SeatBooking : BaseModel
{
    public Showtime Showtime { get; set; } = null!;
    public Seat Seat { get; set; } = null!;
    public SeatStatus Status { get; set; } = SeatStatus.Available;
    public string? Owner;
}

public enum SeatStatus
{
    Available,
    Booked,
    Reserved
}

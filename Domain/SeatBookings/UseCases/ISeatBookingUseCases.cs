using Domain.SeatBookings.Models;
using FluentResults;

namespace Domain.SeatBookings.UseCases;

public interface ISeatBookingUseCases
{
    Task<SeatBooking?> GetSeatBookingById(string id);
    Task<Result<SeatBooking>> BookSeat(SeatBooking seatBooking);
    Task<List<SeatBooking>> ReleaseUnconfirmedSeats();
}

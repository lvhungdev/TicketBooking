using Domain.SeatBookings.Models;
using Domain.Showtimes.Models;
using FluentResults;

namespace Domain.Showtimes.UseCases;

public interface IShowtimeUseCases
{
    Task<Result<List<Showtime>>> GetAllShowtimesInTheater(string theaterId);
    Task<Showtime?> GetShowtimeById(string id);
    Task<Result<Showtime>> CreateShowtime(Showtime showtime);
    Task<Result<List<SeatBooking>>> GetAvailableSeatsForShowtime(string showTimeId);
}

using Domain.Theaters.Models;
using FluentResults;

namespace Domain.Theaters.Ports;

public interface ITheaterRepository
{
    Task<List<Theater>> GetAllTheaters();
    Task<Theater?> GetTheaterById(string id);
    Task<Result<Theater>> CreateTheater(Theater theater);
    Task<Result<Theater>> UpdateTheater(Theater theater);
    Task<Result<string>> DeleteTheater(string id);

    Task<Result<Room>> CreateRoomForTheater(Room room, string theaterId);
    Task<Result<string>> DeleteRoomForTheater(string roomId, string theaterId);
}

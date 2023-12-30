using Domain.Common.Errors;
using Domain.Theaters.Models;
using Domain.Theaters.Ports;
using Domain.Theaters.Validations;
using FluentResults;
using FluentValidation.Results;

namespace Domain.Theaters.UseCases;

public class TheaterUseCases : ITheaterUseCases
{
    private readonly ITheaterRepository theaterRepo;

    public TheaterUseCases(ITheaterRepository theaterRepo)
    {
        this.theaterRepo = theaterRepo;
    }

    public Task<List<Theater>> GetAllTheaters()
    {
        return theaterRepo.GetAllTheaters();
    }

    public Task<Theater?> GetTheaterById(string id)
    {
        return theaterRepo.GetTheaterById(id);
    }

    public async Task<Result<Theater>> CreateTheater(Theater theater)
    {
        ValidationResult validationResult = await new TheaterValidator().ValidateAsync(theater);
        if (!validationResult.IsValid) return Result.Fail(new ValidationFailedError(validationResult.Errors));

        theater.Id = Guid.NewGuid().ToString();
        theater.CreatedAt = DateTimeOffset.Now;
        theater.UpdatedAt = DateTimeOffset.Now;

        return await theaterRepo.CreateTheater(theater);
    }

    public async Task<Result<Theater>> UpdateTheater(Theater theater)
    {
        ValidationResult validationResult = await new TheaterValidator().ValidateAsync(theater);
        if (!validationResult.IsValid) return Result.Fail(new ValidationFailedError(validationResult.Errors));

        Theater? existingTheater = await GetTheaterById(theater.Id);
        if (existingTheater == null) return Result.Fail(new IdNotFoundError(theater.Id));

        existingTheater.UpdatedAt = DateTimeOffset.Now;
        existingTheater.Title = theater.Title;
        existingTheater.Address = theater.Address;

        return await theaterRepo.UpdateTheater(existingTheater);
    }

    public Task<Result<string>> DeleteTheater(string id)
    {
        return theaterRepo.DeleteTheater(id);
    }

    public async Task<Result<Room>> CreateRoomForTheater(Room room, string theaterId)
    {
        ValidationResult validationResult = await new RoomValidator().ValidateAsync(room);
        if (!validationResult.IsValid) return Result.Fail(new ValidationFailedError(validationResult.Errors));

        room.Id = Guid.NewGuid().ToString();
        room.CreatedAt = DateTimeOffset.Now;
        room.UpdatedAt = DateTimeOffset.Now;

        return await theaterRepo.CreateRoomForTheater(room, theaterId);
    }

    public async Task<Result<string>> DeleteRoomForTheater(string roomId, string theaterId)
    {
        Theater? theater = await theaterRepo.GetTheaterById(theaterId);
        if (theater == null) return Result.Fail(new IdNotFoundError(theaterId));

        Room? room = theater.Rooms.FirstOrDefault(m => m.Id == roomId);
        if (room == null) return Result.Fail(new IdNotFoundError(roomId));

        return await theaterRepo.DeleteRoomForTheater(roomId, theaterId);
    }
}

using Domain.Errors;
using Domain.Theaters.Models;
using Domain.Theaters.Ports;
using FluentResults;
using Infrastructure.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Storage.Repositories;

public class TheaterRepository : ITheaterRepository
{
    private readonly AppDbContext dbContext;

    public TheaterRepository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<Theater>> GetAllTheaters()
    {
        List<TheaterEntity> theaters = await dbContext.Theaters
            .Include(t => t.Rooms).ThenInclude(r => r.Seats)
            .ToListAsync();

        return theaters.Select(m => m.MapToTheater()).ToList();
    }

    public async Task<Theater?> GetTheaterById(string id)
    {
        TheaterEntity? theater = await dbContext.Theaters
            .Include(t => t.Rooms).ThenInclude(r => r.Seats)
            .Where(t => t.Id == id)
            .FirstOrDefaultAsync();

        return theater?.MapToTheater();
    }

    public async Task<Result<Theater>> CreateTheater(Theater theater)
    {
        dbContext.Theaters.Add(TheaterEntity.FromTheater(theater));

        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return Result.Fail(new DatabaseError(e));
        }

        return Result.Ok(theater);
    }

    public async Task<Result<Theater>> UpdateTheater(Theater theater)
    {
        TheaterEntity? theaterToUpdate = await dbContext.Theaters.FindAsync(theater.Id);
        if (theaterToUpdate == null) return Result.Fail(new IdNotFoundError(theater.Id));

        theaterToUpdate.Title = theater.Title;
        theaterToUpdate.UpdatedAt = theater.UpdatedAt;
        theaterToUpdate.Title = theater.Title;
        theaterToUpdate.Address = theater.Address;

        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return Result.Fail(new DatabaseError(e));
        }

        return Result.Ok(theater);
    }

    public async Task<Result<string>> DeleteTheater(string id)
    {
        TheaterEntity? theaterToDelete = await dbContext.Theaters.FindAsync(id);
        if (theaterToDelete == null) return Result.Fail(new IdNotFoundError(id));

        dbContext.Theaters.Remove(theaterToDelete);

        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return Result.Fail(new DatabaseError(e));
        }

        return Result.Ok(id);
    }

    public async Task<Result<Room>> CreateRoomForTheater(Room room, string theaterId)
    {
        TheaterEntity? theater = await dbContext.Theaters.FindAsync(theaterId);
        if (theater == null) return Result.Fail(new IdNotFoundError(theaterId));

        theater.Rooms.Add(RoomEntity.FromRoom(room));

        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return Result.Fail(new DatabaseError(e));
        }

        return Result.Ok(room);
    }

    public async Task<Result<string>> DeleteRoomForTheater(string roomId, string theaterId)
    {
        TheaterEntity? theater = await dbContext.Theaters
            .Include(m => m.Rooms)
            .Where(m => m.Id == theaterId)
            .FirstOrDefaultAsync();
        if (theater == null) return Result.Fail(new IdNotFoundError(theaterId));

        RoomEntity? room = theater.Rooms.FirstOrDefault(m => m.Id == roomId);
        if (room == null) return Result.Fail(new IdNotFoundError(roomId));

        theater.Rooms.Remove(room);

        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return Result.Fail(new DatabaseError(e));
        }

        return roomId;
    }
}

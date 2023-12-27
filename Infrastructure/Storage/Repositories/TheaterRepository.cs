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
        TheaterEntity? theater = await dbContext.Theaters.FindAsync(id);

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
}

using API.Controllers.Theaters.Dtos;
using Domain.Theaters.Models;
using Domain.Theaters.UseCases;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Theaters;

[Route("api/theater")]
public class TheaterController(ITheaterUseCases theaterUseCases) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAllTheaters()
    {
        List<Theater> theaters = await theaterUseCases.GetAllTheaters();

        return Ok(theaters);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTheaterById(string id)
    {
        Theater? theater = await theaterUseCases.GetTheaterById(id);

        if (theater == null)
        {
            return new NotFoundResult();
        }

        return Ok(theater);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTheater([FromBody] TheaterReqDto dto)
    {
        Result<Theater> theaterResult = await theaterUseCases.CreateTheater(dto.MapToTheater());

        return theaterResult.IsFailed ? Problem(theaterResult.Errors) : Ok(theaterResult.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTheater(string id, [FromBody] TheaterReqDto dto)
    {
        Theater theater = dto.MapToTheater();
        theater.Id = id;

        Result<Theater> theaterResult = await theaterUseCases.UpdateTheater(theater);

        return theaterResult.IsFailed ? Problem(theaterResult.Errors) : Ok(theaterResult.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMove(string id)
    {
        Result<string> result = await theaterUseCases.DeleteTheater(id);

        return result.IsFailed ? Problem(result.Errors) : Ok();
    }

    [HttpPost("{id}/room")]
    public async Task<IActionResult> CreateRoomForTheater(string id, [FromBody] RoomReqDto dto)
    {
        Result<Room> roomResult = await theaterUseCases.CreateRoomForTheater(dto.MapToRoom(), id);

        return roomResult.IsFailed ? Problem(roomResult.Errors) : Ok(roomResult.Value);
    }

    [HttpDelete("{theaterId}/room/{roomId}")]
    public async Task<IActionResult> DeleteRoomForTheater(string theaterId, string roomId)
    {
        Result<string> result = await theaterUseCases.DeleteRoomForTheater(roomId, theaterId);

        return result.IsFailed ? Problem(result.Errors) : Ok(result.Value);
    }
}

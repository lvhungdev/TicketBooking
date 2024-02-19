using Domain.Theaters.Models;

namespace API.Controllers.Theaters.Dtos;

public class TheaterReqDto
{
    public string Title { get; set; } = null!;
    public string Address { get; set; } = null!;

    public Theater MapToTheater()
    {
        return new Theater { Title = Title, Address = Address };
    }
}

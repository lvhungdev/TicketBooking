namespace Domain.Models;

public class BaseModel
{
    public string Id { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

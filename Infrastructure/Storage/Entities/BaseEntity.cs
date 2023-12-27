namespace Infrastructure.Storage.Entities;

public class BaseEntity
{
    public string Id { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

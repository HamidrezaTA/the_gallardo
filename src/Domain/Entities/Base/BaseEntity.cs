namespace Domain.Entities.Base;
public class BaseEntity<T>
{
    public required T Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}

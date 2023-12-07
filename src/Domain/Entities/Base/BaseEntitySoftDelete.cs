namespace Domain.Entities.Base;
public class BaseEntitySoftDelete<T> : BaseEntity<T>
{
    public DateTimeOffset? DeletedAt { get; set; }
}

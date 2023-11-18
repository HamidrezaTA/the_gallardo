namespace Domain.Entities.Base;

using System;

public class BaseEntitySoftDelete<T> : BaseEntity<T>
{
    public DateTimeOffset? DeletedAt { get; set; }
}

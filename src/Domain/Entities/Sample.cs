using Domain.Entities.Base;

namespace Domain.Entities;
public class Sample : BaseEntitySoftDelete<long>
{
    public required string Title { get; set; }
    public required string Description { get; set; }
}

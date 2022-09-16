using microservice.domain.BaseInterfaces;

namespace microservice.domain.BaseEntities;

public abstract class EntityBase : IEntityBase
{
    public int Id { get; set; }
}

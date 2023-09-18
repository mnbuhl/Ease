using MassTransit;

namespace Eaze.App.Models.Contracts;

public abstract class BaseEntity<TId> : IEntity<TId> where TId : IComparable<TId>
{
    public TId Id { get; protected set; } = default!;
}

public abstract class BaseEntity : BaseEntity<Guid>
{
    protected BaseEntity() => Id = NewId.NextSequentialGuid();
}

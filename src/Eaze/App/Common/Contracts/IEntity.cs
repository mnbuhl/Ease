namespace Eaze.App.Common.Contracts;

public interface IEntity<out TId> where TId : IComparable<TId>
{
    TId Id { get; }
}

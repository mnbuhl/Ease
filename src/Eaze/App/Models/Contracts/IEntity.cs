namespace Eaze.App.Models.Contracts;

public interface IEntity<out TId> where TId : IComparable<TId>
{
    TId Id { get; }
}

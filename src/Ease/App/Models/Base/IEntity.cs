namespace Ease.App.Models.Base;

public interface IEntity<out TId> where TId : IComparable<TId>
{
    TId Id { get; }
}

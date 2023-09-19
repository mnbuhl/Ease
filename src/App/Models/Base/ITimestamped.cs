namespace Ease.App.Models.Base;

public interface ITimestamped
{
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
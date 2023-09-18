namespace Eaze.App.Common.Contracts;

public interface ITimestamped
{
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
namespace Eaze.App.Common.Helpers;

public abstract class Mailable
{
    public string To { get; set; } = default!;
    public List<string> Cc { get; set; } = new();
    public string? Subject { get; set; }
    public string Body { get; set; } = default!;
    public string? FromEmail { get; set; }
    public string? FromName { get; set; }
}
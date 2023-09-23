namespace Ease.Infrastructure.Mailing;

public class Attachment
{
    public string FileName { get; set; } = default!;
    public byte[] Content { get; set; } = default!;
    public string? ContentType { get; set; }

    public Attachment()
    {
    }

    public Attachment(string fileName, byte[] content, string? contentType = null)
    {
        FileName = fileName;
        Content = content;
        ContentType = contentType;
    }
}
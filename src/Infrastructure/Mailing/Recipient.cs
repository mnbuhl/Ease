namespace Ease.Infrastructure.Mailing;

public sealed class Recipient
{
    public string Email { get; set; } = default!;
    public string? Name { get; set; }

    public Recipient()
    {
    }

    public Recipient(string email, string? name = null)
    {
        Email = email;
        Name = name;
    }
}
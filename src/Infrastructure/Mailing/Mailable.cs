namespace Ease.Infrastructure.Mailing;

public abstract class Mailable
{
    private readonly List<Recipient> _to = new();
    private readonly List<Recipient> _cc = new();
    private readonly List<Recipient> _bcc = new();
    private readonly List<Recipient> _replyTo = new();
    private string? _subject;
    private string? _body;
    private Recipient? _from;
    private readonly List<Attachment> _attachments = new();

    public abstract void Build();

    public Mailable To(params string[] email)
    {
        _to.AddRange(email.Select(e => new Recipient(e)));
        return this;
    }

    public Mailable To(params Recipient[] recipient)
    {
        _to.AddRange(recipient);
        return this;
    }

    public Mailable Cc(params string[] email)
    {
        _cc.AddRange(email.Select(e => new Recipient(e)));
        return this;
    }

    public Mailable Cc(params Recipient[] recipient)
    {
        _cc.AddRange(recipient);
        return this;
    }

    public Mailable Bcc(params string[] email)
    {
        _bcc.AddRange(email.Select(e => new Recipient(e)));
        return this;
    }

    public Mailable Bcc(params Recipient[] recipient)
    {
        _bcc.AddRange(recipient);
        return this;
    }

    public Mailable ReplyTo(params string[] email)
    {
        _replyTo.AddRange(email.Select(e => new Recipient(e)));
        return this;
    }

    public Mailable ReplyTo(params Recipient[] recipient)
    {
        _replyTo.AddRange(recipient);
        return this;
    }

    public Mailable Subject(string subject)
    {
        _subject = subject;
        return this;
    }

    public Mailable Body(string body)
    {
        _body = body;
        return this;
    }

    public Mailable From(string email, string? name = null)
    {
        _from = new Recipient(email, name);
        return this;
    }

    public Mailable From(Recipient recipient)
    {
        _from = recipient;
        return this;
    }

    public Mailable Attach(params Attachment[] attachments)
    {
        _attachments.AddRange(attachments);
        return this;
    }

    public void Deconstruct(
        out List<Recipient> to,
        out List<Recipient> cc,
        out List<Recipient> bcc,
        out List<Recipient> replyTo,
        out string? subject,
        out string? body,
        out Recipient? from,
        out List<Attachment> attachments)
    {
        to = _to;
        cc = _cc;
        bcc = _bcc;
        replyTo = _replyTo;
        subject = _subject;
        body = _body;
        from = _from;
        attachments = _attachments;
    }
}
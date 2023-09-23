using Ease.App.Common.Interfaces;

namespace Ease.Infrastructure.Mailing;

public sealed class EmailSender(ILogger<EmailSender> logger) : IEmailSender
{
    public Task<bool> SendAsync(Mailable mailable)
    {
        logger.LogWarning("Email sending is not implemented");

        LogEmail(mailable);

        return Task.FromResult(true);
    }

    public bool Send(Mailable mailable)
    {
        logger.LogWarning("Email sending is not implemented");

        LogEmail(mailable);

        return true;
    }

    public void Queue(Mailable mailable)
    {
        logger.LogWarning("Email sending is not implemented");
        logger.LogWarning("Queue is not implemented");

        LogEmail(mailable);
    }

    private void LogEmail(Mailable mailable)
    {
        mailable.Build();

        var (to, cc, bcc, replyTo, subject, body, from, attachments) = mailable;

        logger.LogInformation(
            "Email Details:\n" +
            "From: {From}\n" +
            "To: {To}\n" +
            "Cc: {Cc}\n" +
            "Bcc: {Bcc}\n" +
            "ReplyTo: {ReplyTo}\n" +
            "Subject: {Subject}\n" +
            "Body: {Body}\n" +
            "Attachments: {Attachments}",
            from?.Email,
            string.Join(", ", to.Select(t => t.Email)),
            string.Join(", ", cc.Select(t => t.Email)),
            string.Join(", ", bcc.Select(t => t.Email)),
            string.Join(", ", replyTo.Select(t => t.Email)),
            subject,
            body,
            string.Join(", ", attachments.Select(a => a.FileName))
        );
    }
}
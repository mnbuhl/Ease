using Ease.App.Common.Helpers;
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

        LogEmail(mailable);
    }

    private void LogEmail(Mailable mailable)
    {
        logger.LogInformation("To: {To}\nSubject: {Subject}\n\n {Body}", mailable.To, mailable.Subject, mailable.Body);
    }
}
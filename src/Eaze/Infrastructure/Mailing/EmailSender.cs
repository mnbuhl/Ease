using Eaze.App.Helpers;
using Eaze.App.Interfaces;

namespace Eaze.Infrastructure.Mailing;

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
        logger.LogInformation("To: {To}", mailable.To);
        logger.LogInformation("Subject: {Subject}", mailable.Subject);
        logger.LogInformation("Body: {Body}", mailable.Body);
    }
}
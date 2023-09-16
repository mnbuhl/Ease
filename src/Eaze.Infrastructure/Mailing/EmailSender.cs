using Eaze.Application.Common.Interfaces;
using Eaze.Application.Common.Models;
using Microsoft.Extensions.Logging;

namespace Eaze.Infrastructure.Mailing;

public sealed class EmailSender(ILogger<EmailSender> logger) : IEmailSender
{
    public Task<bool> SendAsync(Mailable mailable)
    {
        logger.LogWarning("Email sending is not implemented");

        logger.LogInformation("To: {To}", mailable.To);
        logger.LogInformation("Subject: {Subject}", mailable.Subject);
        logger.LogInformation("Body: {Body}", mailable.Body);

        return Task.FromResult(true);
    }

    public bool Send(Mailable mailable)
    {
        logger.LogWarning("Email sending is not implemented");

        logger.LogInformation("To: {To}", mailable.To);
        logger.LogInformation("Subject: {Subject}", mailable.Subject);
        logger.LogInformation("Body: {Body}", mailable.Body);

        return true;
    }

    public void Queue(Mailable mailable)
    {
        logger.LogWarning("Email sending is not implemented");

        logger.LogInformation("To: {To}", mailable.To);
        logger.LogInformation("Subject: {Subject}", mailable.Subject);
        logger.LogInformation("Body: {Body}", mailable.Body);
    }
}
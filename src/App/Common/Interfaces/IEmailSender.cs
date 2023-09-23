using Ease.Infrastructure.Mailing;

namespace Ease.App.Common.Interfaces;

public interface IEmailSender
{
    Task<bool> SendAsync(Mailable mailable);
    bool Send(Mailable mailable);
    void Queue(Mailable mailable);
}
using Eaze.App.Common.Helpers;

namespace Eaze.App.Common.Interfaces;

public interface IEmailSender
{
    Task<bool> SendAsync(Mailable mailable);
    bool Send(Mailable mailable);
    void Queue(Mailable mailable);
}
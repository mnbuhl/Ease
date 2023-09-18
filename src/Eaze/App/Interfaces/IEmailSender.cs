using Eaze.App.Helpers;

namespace Eaze.App.Interfaces;

public interface IEmailSender
{
    Task<bool> SendAsync(Mailable mailable);
    bool Send(Mailable mailable);
    void Queue(Mailable mailable);
}
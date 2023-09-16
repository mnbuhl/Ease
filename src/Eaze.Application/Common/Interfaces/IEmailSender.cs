using Eaze.Application.Common.Models;

namespace Eaze.Application.Common.Interfaces;

public interface IEmailSender
{
    Task<bool> SendAsync(Mailable mailable);
    bool Send(Mailable mailable);
    void Queue(Mailable mailable);
}
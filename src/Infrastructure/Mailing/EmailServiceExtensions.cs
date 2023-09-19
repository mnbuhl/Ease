using Ease.App.Common.Interfaces;

namespace Ease.Infrastructure.Mailing;

public static class EmailServiceExtensions
{
    public static void AddEmailServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IEmailSender, EmailSender>();
    }
}
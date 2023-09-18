using Eaze.App.Common.Interfaces;

namespace Eaze.Infrastructure.Mailing;

public static class EmailServiceExtensions
{
    public static void AddEmailServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IEmailSender, EmailSender>();
    }
}
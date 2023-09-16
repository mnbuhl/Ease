using Eaze.Application.Common.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Eaze.Infrastructure.Mailing;

public static class EmailServiceExtensions
{
    public static void AddEmailServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IEmailSender, EmailSender>();
    }
}
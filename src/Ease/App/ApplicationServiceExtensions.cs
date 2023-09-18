using Ease.App.Requests;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Ease.App;

public static class ApplicationServiceExtensions
{
    public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
    {
        // Register application services such as Validators, MediatR handlers, etc.
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddValidatorsFromAssemblyContaining<RegisterValidator>();

        return builder;
    }
}
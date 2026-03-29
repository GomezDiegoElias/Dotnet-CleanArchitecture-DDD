using System.Diagnostics;

using BuberDinner.Api.Common.Errors;
using BuberDinner.Api.Common.Http;
using BuberDinner.Api.Common.Mapping;

using ErrorOr;

using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BuberDinner.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        // < .net 8
        //services.AddSingleton<ProblemDetailsFactory, BuberDinnerProblemDetailsFactory>();

        // .net 8+
        services.AddExceptionHandler<GlobalExceptionHandler>(); // Register our global exception handler which will be invoked by the default exception handler middleware

        // Customize the problem details response to include additional information such as traceId and errorCodes
        // Replace the default problem details factory with our custom implementation that can handle the additional information
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = ctx =>
            {
                ctx.ProblemDetails.Extensions["traceId"] = Activity.Current?.Id ?? ctx.HttpContext.TraceIdentifier;

                if (ctx.HttpContext.Items[HttpContextItemKeys.Errors] is List<Error> errors)
                {
                    ctx.ProblemDetails.Extensions["errorCodes"] = errors.Select(e => e.Code);
                }
            };
        });

        services.AddOpenApi();

        services.AddMappings();
        return services;
    }
}
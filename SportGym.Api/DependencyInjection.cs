using System.Diagnostics;

using ErrorOr;

using Microsoft.AspNetCore.Mvc.Infrastructure;

using SportGym.Api.Common.Errors;
using SportGym.Api.Common.Http;
using SportGym.Api.Common.Mapping;

namespace SportGym.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        // < .net 8
        //services.AddSingleton<ProblemDetailsFactory, SportGymProblemDetailsFactory>();

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

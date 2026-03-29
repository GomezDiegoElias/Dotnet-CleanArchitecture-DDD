using System.Reflection;

using ErrorOr;

using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Common.Behaviors;

namespace BuberDinner.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Old syntax in MediatR 14.1.0, not working with MediatR.Extensions.Microsoft.DependencyInjection 11.1.0
        //services.AddMediatR(typeof(DependencyInjection).Assembly);
        //services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        //services.AddMediatR(m => m.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)));

        // New syntax in MediatR 14.1.0, not working with MediatR.Extensions.Microsoft.DependencyInjection 11.1.0
        // Now we need to install MediatR.Extensions.Microsoft.DependencyInjection 12.0.0, but it is not compatible with .NET 6, so we will stick to the old syntax for now.
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidateBehavior<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
